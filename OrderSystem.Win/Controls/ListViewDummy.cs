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
    }
}