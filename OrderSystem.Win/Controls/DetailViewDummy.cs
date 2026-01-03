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
            List<IDataControl> toLoad = GetControls(Root);
            foreach (IDataControl control in toLoad)
            {
                if (control is IComplexDataControl complex)
                {
                    complex.LoadData(entity, sp);
                }
                else
                {
                    control.LoadData(entity);
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

        private List<IDataControl> GetControls(Control root, List<IDataControl>? items = null)
        {
            items ??= [];

            foreach (Control nested in root.Controls)
            {
                if (nested is IDataControl load)
                {
                    items.Add(load);
                }

                items = GetControls(nested, items);
            }

            return items;
        }
    }
}