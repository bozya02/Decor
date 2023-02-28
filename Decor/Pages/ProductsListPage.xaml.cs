using Decor.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Decor.Pages
{
    /// <summary>
    /// Interaction logic for ProductsListPage.xaml
    /// </summary>
    public partial class ProductsListPage : Page
    {
        private List<Product> _products { get; set; }
        public List<Product> Products { get; set; }

        /// <summary>
        /// Сортировки
        /// </summary>
        public Dictionary<string, Func<Product, object>> Sortings { get; set; }

        /// <summary>
        /// Фильтры
        /// </summary>
        public Dictionary<string, Predicate<Product>> Filters { get; set; }

        public ProductsListPage()
        {
            InitializeComponent();

            _products = DataAccess.GetProducts();
            Products = _products.ToList();

            Sortings = new Dictionary<string, Func<Product, object>>
            {
                { "Без сортировки", x => x.Id },
                { "Цена по убыванию", x => x.Price },
                { "Цена по возрастанию", x => x.Price }
            };
            
            Filters = new Dictionary<string, Predicate<Product>>
            {
                { "Все диапазоны", x => 0 < x.ActualDiscount },
                { "0-10%", x => x.ActualDiscount <= 10 },
                { "11-14%", x => 11 <= x.ActualDiscount && x.ActualDiscount <= 14 },
                { "15% и более", x => 15 <= x.ActualDiscount }
            };

            if (App.User == null || App.User.Role.Name == "Клиент" || App.User.Role.Name == "Менеджер" || App.User.Role.Name == "Сотрудник")
                btnAddProduct.Visibility = Visibility.Collapsed;

            this.DataContext = this;
            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;
        }

        private void DataAccess_AddNewItemEvent()
        {
            _products = DataAccess.GetProducts();
            ApplyFilters();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var search = tbSearch.Text.ToLower();
            var sorting = cbSorting.SelectedItem as string;
            var filter = cbFilter.SelectedItem as string;

            if (string.IsNullOrEmpty(sorting) || string.IsNullOrEmpty(filter))
                return;

            Products = _products.FindAll(Filters[filter]).FindAll(x => x.Name.ToLower().Contains(search) ||
                                                                       x.Article.ToLower().Contains(search) ||
                                                                       x.Description.ToLower().Contains(search));

            Products = sorting.Contains("убыванию") ?
                       Products.OrderByDescending(Sortings[sorting]).ToList() :
                       Products.OrderBy(Sortings[sorting]).ToList();

            lvProducts.ItemsSource = Products;
            lvProducts.Items.Refresh();

            tbItems.Text = $"{Products.Count} / {_products.Count}";
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = lvProducts.SelectedItem as Product;
            if (product != null)
                NavigationService.Navigate(new ProductPage(product));

            lvProducts.SelectedIndex = -1;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(new Product(), false));
        }
    }
}
