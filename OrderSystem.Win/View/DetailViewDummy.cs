using OrderSystem.Core.Entities;

namespace OrderSystem.Win.View
{
    public class DetailViewDummy : UserControl
    {
        public DetailViewDummy()
        {
        }

        private readonly IServiceProvider sp;

        public Control Root
        {
            get { return this; }
        }

        public virtual void LoadData<TEntity>(TEntity entity)
        {
            List<IDataControl> toLoad = GetControls(Root);
            foreach (IDataControl control in toLoad)
            {
                control.LoadData(entity);
            }
        }

        public virtual Result Evaluate()
        {
            return Result.Fail("Invalid");
        }

        public virtual object ReadData()
        {
            List<IDataControl> controls = GetControls(Root);
            return null;
        }

        protected List<IDataControl> GetControls(Control root, List<IDataControl>? items = null)
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