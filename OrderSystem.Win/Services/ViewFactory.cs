using OrderSystem.Core.Entities;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Win.View;
using OrderSystem.Win.Controls;
using OrderSystem.Win.ViewControllers;

namespace OrderSystem.Win.Services
{
    public class ViewFactory
    {
        private readonly IServiceProvider sp;

        private readonly Dictionary<Type, Type> detailViewMapping;
        private readonly Dictionary<string, Type> entityMapping;
        private readonly Dictionary<Type, PropertyInfo> identifierMapping;
        private readonly Dictionary<Type, List<Type>> controllerMapping;

        public ViewFactory(IServiceProvider sp)
        {
            this.sp = sp;

            List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(SafeGetTypes)
                                        .ToList();

            List<Type> entities = types
                                 .Where(x => x.IsAssignableTo(typeof(PersistentEntityBase)))
                                 .ToList();

            List<Type> controllers = types
                                    .Where(x => x.IsAssignableTo(typeof(IControllerBase)) &&
                                                x is { IsAbstract: false, IsInterface: false })
                                    .ToList();

            controllerMapping = [];
            foreach (Type controller in controllers)
            {
                Type? entityType = TryGetEntityTypeFromController(controller);
                if (entityType is null)
                {
                    continue;
                }

                if (!controllerMapping.TryGetValue(entityType, out List<Type>? list))
                {
                    list = [];
                    controllerMapping[entityType] = list;
                }

                list.Add(controller);
            }

            identifierMapping = entities.Select(x =>
            {
                Type t = x;
                PropertyInfo[] props = x.GetProperties();
                PropertyInfo idProp = props.Single(prop => prop.Name == nameof(PersistentEntityBase.Id));

                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetCustomAttribute<IdentifierAttribute>() == null)
                    {
                        continue;
                    }

                    idProp = prop;
                    break;
                }

                return (t, idProp);
            }).ToDictionary();

            entityMapping = entities
                           .Select(x => (x.Name, x))
                           .ToDictionary();

            detailViewMapping = types.Where(t => !t.IsAbstract && typeof(DetailViewDummy).IsAssignableFrom(t))
                                     .Select(t => new
                                     {
                                         Type = t,
                                         Attr = t.GetCustomAttributes(typeof(DetailViewAttribute), true)
                                                  .Cast<DetailViewAttribute>()
                                                  .SingleOrDefault()
                                     })
                                     .Where(x => x.Attr != null)
                                     .ToDictionary(x => x.Attr!.Type, x => x.Type);
        }

        private static IEnumerable<Type> SafeGetTypes(Assembly a)
        {
            try { return a.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { return ex.Types.Where(t => t != null)!; }
        }

        public Type GetTypeByName(string name)
        {
            entityMapping.TryGetValue(name, out Type? type);
            return type ?? typeof(PersistentEntityBase);
        }

        public PropertyInfo? GetIdentifier(Type type)
        {
            identifierMapping.TryGetValue(type, out PropertyInfo? info);
            return info;
        }

        public DetailView<TEntity> CreateDetailView<TEntity>() where TEntity : PersistentEntityBase
        {
            Type entityType = typeof(TEntity);
            if (!detailViewMapping.TryGetValue(entityType, out Type? templateType))
            {
                throw new InvalidOperationException($"No DetailView registered for {entityType.Name}");
            }

            object? template = Activator.CreateInstance(templateType);
            if (template is not DetailViewDummy dummy)
            {
                throw new InvalidOperationException($"Could not create Template for {entityType.Name}");
            }

            return ActivatorUtilities.CreateInstance<DetailView<TEntity>>(sp, dummy);
        }

        public List<IControllerBase> MakeControllers<TEntity>(ViewBase viewBase) where TEntity : PersistentEntityBase
        {
            Type entityType = typeof(TEntity);

            if (!controllerMapping.Keys.Contains(entityType))
            {
                throw new InvalidOperationException($"Unknown Type {entityType.Name}");
            }

            List<IControllerBase> output = [];
            foreach (Type controllerType in controllerMapping[entityType])
            {
                object? createdInstance = ActivatorUtilities.CreateInstance(sp, controllerType, viewBase);
                if (createdInstance is IControllerBase controller)
                {
                    output.Add(controller);
                }
            }

            return output;
        }

        private static Type? TryGetEntityTypeFromController(Type controllerType)
        {
            Type? bt = controllerType.BaseType;
            while (bt != null)
            {
                if (bt.IsGenericType && bt.GetGenericTypeDefinition() == typeof(ViewController<>))
                {
                    Type arg = bt.GetGenericArguments()[0];
                    return arg;
                }
                bt = bt.BaseType;
            }
            return null;
        }
    }
}