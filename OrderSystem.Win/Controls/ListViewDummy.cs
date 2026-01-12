using OrderSystem.Win.View;

namespace OrderSystem.Win.Controls
{
    /// <summary>
    /// design-time representation of a <seealso cref="ListView{TEntity}"/>. Replaced with the actual component during construction of a <seealso cref="DetailView{TEntity}"/>
    /// </summary>
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