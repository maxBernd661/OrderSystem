using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.Win.Controls;
using OrderSystem.Win.Forms;
using OrderSystem.Win.Services;
using System.ComponentModel;
using System.Reflection;
using System.Security.Principal;

namespace OrderSystem.Win.View
{
    public class ListView<T> : ViewBase, IListView where T : PersistentEntityBase
    {
        private readonly ViewManager viewManager;

        public ListView(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            viewManager = serviceProvider.GetRequiredService<ViewManager>();
            InitializeCore<T>();
            InitializeListView();
            Load += async (_, _) => { await LoadSourceData(); };
        }

        public ListView(IServiceProvider serviceProvider, Func<T, bool> filter) : this(serviceProvider)
        {
            this.Filter = filter;
        }

        public Func<T, bool>? Filter { get; private set; }

        public void RefreshItem(PersistentEntityBase item)
        {
            if (item is not T converted)
            {
                return;
            }

            if (Source.DataSource is not IList<T> list)
            {
                return;
            }

            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == converted.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                return;
            }

            list[index] = converted;
            Source.ResetItem(index);
        }

        public DataGridView Grid { get; set; }

        public BindingSource Source { get; set; }

        public event EventHandler<CustomOpenEventArgs<T>>? OnCustomOpenDetailView;

        public bool HasData
        {
            get
            {
                return unorderedData.Count > 0;
            }
        }

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
                DataSource = Source,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            };

            Grid.Dock = DockStyle.Fill;

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            MinimumSize = new Size(300, 200);
            Size = new Size(300, 200);

            Controls.Add(Grid);

            Grid.SelectionChanged += (sender, args) => OnChanged();

            Grid.ColumnHeaderMouseClick += (_, args) =>
            {
                ListViewColumn? column = (ListViewColumn)Grid.Columns[args.ColumnIndex];
                OrderByColumn(column);
            };

            Grid.CellDoubleClick += async (_, args) =>
            {
                if (args.RowIndex < 0 || args.ColumnIndex < 0)
                {
                    return;
                }

                if (Grid.Rows[args.RowIndex].DataBoundItem is not T entity)
                {
                    return;
                }

                if (OnCustomOpenDetailView != null)
                {
                    OnCustomOpenDetailView(this, new CustomOpenEventArgs<T>(entity));
                    return;
                }

                ViewHolder holder = await viewManager.AddDetailView(entity);
                ServiceProvider.GetRequiredService<MainForm>().ShowView(holder);
            };
        }

        public List<PersistentEntityBase> GetData()
        {
            return unorderedData.OfType<PersistentEntityBase>().ToList();
        }

        public async Task LoadSourceData(object? data = null)
        {
            Grid.Columns.Clear();

            List<T>? dbData;
            if (data is null)
            {
                dbData = await ServiceProvider.GetRequiredService<OrderContext>().Set<T>().AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
            }
            else
            {
                dbData = TryGetListData(data).ToList();
            }

            if (Filter != null)
            {
                List<T> filtered = dbData.Where(Filter).ToList();
                dbData = filtered;
            }

            Source.DataSource = dbData;
            unorderedData = dbData;

            Dictionary<PropertyInfo, string> props = typeof(T).GetProperties()
                                                              .Select(x => (x, x.GetCustomAttribute<ColumnNameAttribute>()?.Name ?? x.Name))
                                                              .ToDictionary();

            List<string> basePropNames = typeof(PersistentEntityBase).GetProperties().Select(x => x.Name).ToList();

            foreach (KeyValuePair<PropertyInfo, string> prop in props)
            {
                if (prop.Key.GetCustomAttribute<HideInListViewAttribute>() != null)
                {
                    continue;
                }

                ListViewColumn propColumn = new()
                {
                    DataPropertyName = prop.Key.Name,
                    HeaderText = prop.Value,
                    ReadOnly = true,
                    BackingProperty = prop.Key,
                    ColumnText = prop.Value,
                    Visible = !basePropNames.Contains(prop.Key.Name)
                };

                Grid.Columns.Add(propColumn);
            }
        }

        private IEnumerable<T> TryGetListData(object data)
        {
            Type targetType = typeof(T);
            Type objectType = data.GetType();

            PropertyInfo? listProp = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x =>
            {
                Type propType = x.PropertyType;

                if (propType == typeof(string))
                {
                    return false;
                }

                if (propType.IsGenericType &&
                    propType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    return propType.GetGenericArguments()[0] == targetType;
                }

                Type? enumerableInterface = propType
                                           .GetInterfaces()
                                           .FirstOrDefault(i =>
                                                i.IsGenericType &&
                                                i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                return enumerableInterface?.GetGenericArguments()[0] == targetType;
            });

            if (listProp is null)
            {
                return [];
            }

            return listProp.GetValue(data) as IEnumerable<T> ?? [];
        }

        public Guid Ident { get; private set; }

        public void SetIdent(Guid id)
        {
            Ident = id;
        }

        public void AddControl(Control control)
        {
            Controls.Add(control);
        }

        public PersistentEntityBase? GetSelectedItem()
        {
            if (Grid.SelectedRows.Count == 0)
            {
                return null;
            }

            return Grid.SelectedRows[0].DataBoundItem as PersistentEntityBase;
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

    public class CustomOpenEventArgs<T>(T data) : EventArgs where T : PersistentEntityBase
    {
        public T Data { get; } = data;
    }

    public interface IListView
    {
        public Guid Ident { get; }

        public void SetIdent(Guid ident);

        public void AddControl(Control control);

        public PersistentEntityBase? GetSelectedItem();

        public void RefreshItem(PersistentEntityBase item);

        public DataGridView Grid { get; }

        public bool HasData { get; }

        public List<PersistentEntityBase> GetData();

        public Task LoadSourceData(object? data = null);
    }
}