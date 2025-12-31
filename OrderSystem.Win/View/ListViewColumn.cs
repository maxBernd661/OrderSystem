using System.Reflection;

namespace OrderSystem.Win.View
{
    public class ListViewColumn : DataGridViewColumn
    {
        public ListViewColumn() : base(new DataGridViewTextBoxCell())
        {
        }

        public PropertyInfo BackingProperty { get; set; }

        public SortingDirection Direction { get; set; }

        public string ColumnText { get; set; }
    }
}