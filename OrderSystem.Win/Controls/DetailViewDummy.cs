using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    /// <summary>
    /// design-time representation of a <seealso cref="DetailView{TEntity}"/>.
    /// </summary>
    public class DetailViewDummy : UserControl
    {
        public Control Root
        {
            get { return this; }
        }

        public event EventHandler<EventArgs> Changed;

        protected void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sends data from a <seealso cref="PersistentEntityBase"/> to all nested controls
        /// </summary>
        public void LoadData<TEntity>(TEntity? entity)
        {
            List<Control> toLoad = GetControls(Root);
            foreach (Control control in toLoad)
            {
                if (control is IDataControl dataControl)
                {
                    dataControl.LoadData(entity);
                }
                else if (control is IListView listView)
                {
                    listView.LoadSourceData(entity);
                }
            }
        }

        public virtual Result Evaluate()
        {
            return Result.Fail("Invalid");
        }

        public virtual object ReadData()
        {
            return string.Empty;
        }

        /// <summary>
        /// Returns all nested controls
        /// </summary>
        private List<Control> GetControls(Control root, List<Control>? items = null)
        {
            items ??= [];

            foreach (Control nested in root.Controls)
            {
                items.Add(nested);
                items = GetControls(nested, items);
            }

            return items;
        }

        /// <summary>
        /// Returns a nested <seealso cref="ListView{TEntity}"/> for the given guid, or <c>null</c> if none is found
        /// </summary>
        protected IListView? GetListView(Guid ident)
        {
            List<IListView> toCheck = GetControls(this).OfType<IListView>().ToList();
            return toCheck.FirstOrDefault(x => x.Ident == ident);
        }
    }
}