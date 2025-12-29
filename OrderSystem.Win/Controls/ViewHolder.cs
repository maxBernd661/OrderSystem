using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public class ViewHolder : TabPage
    {
        public ViewHolder(string name, ViewBase view) : base(name)
        {
            View = view;
            View.Dock = DockStyle.Fill;
            Controls.Add(View);
        }

        public ViewBase View { get; }
    }
}