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
        protected IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected ViewHolder holder;
        protected readonly IServiceProvider sp;

        protected ViewBase()
        {
        }

        [ActivatorUtilitiesConstructor]
        protected ViewBase(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public ViewHolder Holder
        {
            get { return holder; }
        }

        public void SetHolder(ViewHolder holder)
        {
            this.holder = holder;
        }

        public virtual ViewKind Kind
        {
            get { return ViewKind.ListView; }
        }

        public virtual Task LoadData(Guid? id = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task SaveData()
        {
            return Task.CompletedTask;
        }

        protected void InitializeCore<T>()
        {
            EntityType = typeof(T);
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