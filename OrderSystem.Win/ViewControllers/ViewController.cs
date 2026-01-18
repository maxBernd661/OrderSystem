using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.ViewControllers
{
    public abstract class ViewController<TEntity> : IControllerBase where TEntity : PersistentEntityBase
    {
        protected ViewController(ViewBase view)
        {
            View = view;
            View.Changed += ViewOnChanged;
            View.Load += ViewOnLoad;
        }

        protected virtual void ViewOnLoad(object? sender, EventArgs e)
        {
        }

        protected virtual void ViewOnChanged(object? sender, EventArgs e)
        {
        }

        public ViewBase View { get; }

        public virtual void Dispose()
        {
            View.Changed -= ViewOnChanged;
            View.Load -= ViewOnLoad;
        }

        protected void SetViewChanged()
        {
            View.SetChanged();
        }
    }

    public interface IControllerBase : IDisposable
    {
        public ViewBase View { get; }
    }
}