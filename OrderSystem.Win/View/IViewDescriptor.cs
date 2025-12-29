using OrderSystem.Win.Forms;

namespace OrderSystem.Win.View
{
    public interface IViewDescriptor
    {
        ViewKind ViewKind { get; }

        Type ViewType { get; }

        Type EntityType { get; }

        string Title { get; }
    }

    public sealed class ViewDescriptor<TView, TEntity>(ViewKind kind, string title) : IViewDescriptor where TView : IView
    {
        public ViewKind ViewKind { get; } = kind;

        public Type ViewType
        {
            get { return typeof(TView); }
        }

        public Type EntityType
        {
            get { return typeof(TEntity); }
        }

        public string Title { get; } = title;
    }

    public enum ViewKind
    {
        DetailView,
        ListView
    }
}