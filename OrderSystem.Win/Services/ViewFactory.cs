using OrderSystem.Core.Entities;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Services
{
    public class ViewFactory
    {
        private readonly IServiceProvider sp;

        private readonly Dictionary<Type, Type> detailViewMapping;
        private readonly Dictionary<string, Type> entityMapping;
        private readonly Dictionary<Type, PropertyInfo> identifierMapping;

        public ViewFactory(IServiceProvider sp)
        {
            this.sp = sp;

            List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(SafeGetTypes)
                                        .ToList();

            List<Type> entities = types
                                 .Where(x => x.IsAssignableTo(typeof(PersistentEntityBase))).ToList();

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
    }
}