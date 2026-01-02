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
        private ViewHolder? holder;

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

        protected ViewHolder? Holder
        {
            get { return holder; }
        }

        public void SetHolder(ViewHolder viewHolder)
        {
            holder = viewHolder;
        }

        public virtual ViewKind Kind
        {
            get { return ViewKind.ListView; }
        }

        protected void InitializeCore<T>()
        {
            EntityType = typeof(T);
        }

        public Type EntityType { get; private set; } = typeof(PersistentEntityBase);

        public virtual Result QueryCanClose()
        {
            return Result.Fail("No reason given");
        }

        public virtual void Close()
        {
            throw new InvalidOperationException();
        }
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