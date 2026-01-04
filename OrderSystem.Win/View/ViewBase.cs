using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;

namespace OrderSystem.Win.View
{
    public class ViewBase : UserControl
    {
        protected IContainer? components = null;

        public event EventHandler<EventArgs>? Changed;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        protected IServiceProvider ServiceProvider { get; }

        [ActivatorUtilitiesConstructor]
        protected ViewBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public ViewHolder? Holder { get; private set; }

        public void SetHolder(ViewHolder viewHolder)
        {
            Holder = viewHolder;
        }

        public virtual ViewKind Kind
        {
            get { return ViewKind.ListView; }
        }

        protected void InitializeCore<T>()
        {
            EntityType = typeof(T);
        }

        public List<Control> GetControls()
        {
            return GetControlsRoot(this);
        }

        private List<Control> GetControlsRoot(Control root, List<Control>? items = null)
        {
            items ??= [];

            foreach (Control nested in root.Controls)
            {
                items.Add(nested);
                items = GetControlsRoot(nested, items);
            }

            return items;
        }

        public Type EntityType { get; private set; } = typeof(PersistentEntityBase);
    }

    public enum SortingDirection
    {
        None,
        Ascending,
        Descending
    }

    public static class ReflectionExtensions
    {
        public static List<T> OrderByProperty<T>(this IEnumerable<T> source, PropertyInfo prop)
        {
            return source.OrderBy(x => prop.GetValue(x, null)).ToList();
        }

        public static List<T> OrderByPropertyDescending<T>(this IEnumerable<T> source, PropertyInfo prop)
        {
            return source.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }
    }
}