using OrderSystem.Win.Forms;
using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    public class ViewHolder : TabPage
    {
        public ViewHolder(string name, ViewBase view, MainForm form) : base(name)
        {
            View = view;
            View.Dock = DockStyle.Fill;
            View.SetHolder(this);

            Controls.Add(View);
            MainForm = form;
        }

        public ViewBase View { get; }

        public MainForm MainForm { get; }
    }
}