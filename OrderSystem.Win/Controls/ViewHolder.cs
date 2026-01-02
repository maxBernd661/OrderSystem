using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public class ViewHolder : TabPage
    {
        public event EventHandler<EventArgs> ViewChanged;

        public ViewHolder(string name, ViewBase view) : base(name)
        {
            View = view;

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
}