using OrderSystem.Core.Entities;

namespace OrderSystem.Win.Controls
{
    public class ListViewDummy : UserControl
    {
        public ListViewDummy()
        {
        }

        public string EntityType { get; set; }

        public string FilterKey { get; set; }

        public bool OnlyRelevantData { get; set; }
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;

        public Guid Ident { get; } = Guid.NewGuid();

        private void InitializeComponent()
        {
        }
    }
}