using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;

namespace OrderSystem.Win.View
{
    public class ViewBase : UserControl, IView
    {
        protected IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected ViewHolder holder;
        protected readonly OrderContext context;

        protected ViewBase()
        {
        }

        [ActivatorUtilitiesConstructor]
        protected ViewBase(OrderContext context)
        {
            this.context = context;
        }

        public ViewHolder Holder
        {
            get { return holder; }
        }

        public void SetHolder(ViewHolder holder)
        {
            this.holder = holder;
        }

        public virtual ViewKind Kind
        {
            get { return ViewKind.ListView; }
        }

        public virtual Task LoadData(Guid? id = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task SaveData()
        {
            return Task.CompletedTask;
        }

        protected void InitializeCore<T>()
        {
            EntityType = typeof(T);
        }

        public Type EntityType { get; private set; } = typeof(PersistentEntityBase);
    }

    public class ListView<T> : ViewBase where T : PersistentEntityBase
    {
        public ListView(OrderContext context) : base(context)
        {
            InitializeCore<T>();
            InitializeListView();
            Load += async (_, _) => { await LoadSourceData(); };
        }

        public DataGridView Grid { get; set; }

        public BindingSource Source { get; set; }

        private List<T> unorderedData;

        private void InitializeListView()
        {
            components = new Container();
            Source = new BindingSource(components);

            Grid = new DataGridView()
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                AllowUserToResizeColumns = true,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(224, 224, 224),
                ReadOnly = true,
                Size = new Size(300, 200),
                DataSource = Source
            };

            Grid.Dock = DockStyle.Fill;

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            MinimumSize = new Size(300, 200);
            Size = new Size(300, 200);

            Controls.Add(Grid);

            Grid.ColumnHeaderMouseClick += (sender, args) =>
            {
                ListViewColumn? column = (ListViewColumn)Grid.Columns[args.ColumnIndex];
                OrderByColumn(column);
            };

            Grid.CellDoubleClick += (sender, args) =>
            {
            };
        }

        private async Task LoadSourceData()
        {
            List<T> dbData = await context.Set<T>().AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
            Source.DataSource = dbData;
            unorderedData = dbData;

            Dictionary<PropertyInfo, string> props = typeof(T).GetProperties()
                                                                 .Select(x => (x, x.GetCustomAttribute<ColumnNameAttribute>()?.Name ?? x.Name))
                                                                 .ToDictionary();

            List<string> basePropNames = typeof(PersistentEntityBase).GetProperties().Select(x => x.Name).ToList();

            foreach (KeyValuePair<PropertyInfo, string> prop in props)
            {
                ListViewColumn propColumn = new()
                {
                    DataPropertyName = prop.Key.Name,
                    HeaderText = prop.Value,
                    ReadOnly = true,
                    BackingProperty = prop.Key,
                    ColumnText = prop.Value,
                };

                Grid.Columns.Add(propColumn);
            }
        }

        private void OrderByColumn(ListViewColumn column)
        {
            foreach (ListViewColumn existingColumn in Grid.Columns.OfType<ListViewColumn>())
            {
                existingColumn.HeaderText = existingColumn.ColumnText;
            }

            List<T> curList;

            if (column.Direction is SortingDirection.None)
            {
                curList = unorderedData.OrderByProperty(column.BackingProperty).ToList();
                column.HeaderText += @" (↑)";
                column.Direction = SortingDirection.Ascending;
            }
            else if (column.Direction is SortingDirection.Ascending)
            {
                curList = unorderedData.OrderByPropertyDescending(column.BackingProperty).ToList();
                column.HeaderText += @" (↓)";
                column.Direction = SortingDirection.Descending;
            }
            else
            {
                curList = unorderedData;
                column.Direction = SortingDirection.None;
            }

            Source.DataSource = curList;
        }
    }

    public class ListViewDummy : UserControl
    {
        public ListViewDummy()
        {
        }

        private readonly IServiceProvider sp;

        [ActivatorUtilitiesConstructor]
        public ListViewDummy(IServiceProvider sp) : this()
        {
            this.sp = sp;

            Control? parent = Parent;
            if (parent is null)
            {
                return;
            }

            Load += (sender, args) =>
            {
                int parentIndex = parent.Controls.GetChildIndex(this);
                parent.Controls.Remove(this);

                Type entityType = Type.GetType(EntityType) ?? typeof(PersistentEntityBase);
                Type listviewType = typeof(ListView<>).MakeGenericType(entityType);
                Control listView = (Control)ActivatorUtilities.CreateInstance(sp, listviewType);

                listView.Dock = DockStyle.Fill;
                parent.Controls.Add(listView);
                parent.Controls.SetChildIndex(listView, parentIndex);

                Dispose();
            };
        }

        public string EntityType { get; set; }
    }

    public class DetailView : ViewBase
    {
        public DetailView()
        {
        }

        [ActivatorUtilitiesConstructor]
        public DetailView(OrderContext context) : base(context)
        {
        }

        public override ViewKind Kind
        {
            get { return ViewKind.DetailView; }
        }
    }

    public enum SortingDirection
    {
        None,
        Ascending,
        Descending
    }

    public class ListViewColumn : DataGridViewColumn
    {
        public ListViewColumn() : base(new DataGridViewTextBoxCell())
        {
        }

        public PropertyInfo BackingProperty { get; set; }

        public SortingDirection Direction { get; set; }

        public string ColumnText { get; set; }
    }

    public static class ReflectionExtensions
    {
        public static List<T> OrderByProperty<T>(this IEnumerable<T> source, PropertyInfo prop)
        {
            return source.OrderBy(x => prop.GetValue(x, null)).ToList();
        }

        public static List<T> OrderByPropertyDescending<T>(this IEnumerable<T> source, PropertyInfo prop)
        {
            return source.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }
    }
}