using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public class ViewHolder : UserControl
    {
        public event EventHandler<EventArgs>? ViewChanged;

        public string Name { get; }

        public ViewHolder(string name, ViewBase view)
        {
            View = view;
            Name = name;

            View.Dock = DockStyle.Fill;
            View.SetHolder(this);

            if (View is IDetailView dv)
            {
                dv.Changed += (_, _) => OnViewChanged();
            }

            Controls.Add(View);
        }

        public ViewBase View { get; }

        private void OnViewChanged()
        {
            ViewIsChanged = true;
            ViewChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool ViewIsChanged { get; private set; }
    }

    public class TabPageHost : TabPage
    {
        public ViewHolder Holder { get; }

        public TabPageHost(string title, ViewHolder holder) : base(title)
        {
            Holder = holder;
            Holder.Dock = DockStyle.Fill;
            Controls.Add(Holder);
        }
    }
}