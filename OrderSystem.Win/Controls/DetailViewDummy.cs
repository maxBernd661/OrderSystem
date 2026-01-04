using OrderSystem.Core.Entities;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
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

        public void LoadData<TEntity>(TEntity? entity, IServiceProvider sp)
        {
            List<Control> toLoad = GetControls(Root);
            foreach (Control control in toLoad)
            {
                if (control is IComplexDataControl complex)
                {
                    complex.LoadData(entity, sp);
                }
                else if (control is IDataControl dataControl)
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
    }
}