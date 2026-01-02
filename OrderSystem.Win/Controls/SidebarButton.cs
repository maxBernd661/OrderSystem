namespace OrderSystem.Win.Controls
{
    public partial class SidebarButton : UserControl
    {
        public SidebarButton()
        {
            InitializeComponent();
        }

        public string DisplayText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }

        public Color NormalColor { get; set; } = Color.White;

        public Color HighlightColor { get; set; } = Color.LightBlue;

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = HighlightColor;
        }

        private void OnMouseLeave(object? sender, EventArgs e)
        {
            BackColor = NormalColor;
        }

        public event EventHandler<EventArgs> ButtonClicked;

        private void OnClicked()
        {
            ButtonClicked.Invoke(this, EventArgs.Empty);
        }
    }
}