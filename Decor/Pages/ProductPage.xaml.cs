using Decor.DB;
using Microsoft.Win32;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Unit> Units { get; set; }


        public ProductPage(Product product, bool isNew = false)
        {
            InitializeComponent();

            Categories = DataAccess.GetCategories();
            Producers = DataAccess.GetProducers();
            Suppliers = DataAccess.GetSuppliers();
            Units = DataAccess.GetUnits();

            Product = product;

            if (isNew)
            {
                Title = $"Новый {Title}";
                btnDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                tbArticle.IsEnabled = false;
                Title += $" {Product.Article}";
                btnDelete.Visibility = Product.ProductOrders.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }

            if (App.User == null || App.User.Role.Name == "Клиент" || App.User.Role.Name == "Менеджер" || App.User.Role.Name == "Сотрудник")
                this.IsEnabled = false;

            this.DataContext = this;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };

            if (fileDialog.ShowDialog().Value)
            {
                Product.Image = File.ReadAllBytes(fileDialog.FileName);
                imgImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Product.Article) || Product.Article.Length != 6 || DataAccess.IsExistActicle(Product))
                stringBuilder.AppendLine("Неверный артикул!");
            if (string.IsNullOrWhiteSpace(Product.Name))
                stringBuilder.AppendLine("Неверное наименование!");
            if (Product.Category == null)
                stringBuilder.AppendLine("Категория не выбрана!");
            if (Product.Unit == null)
                stringBuilder.AppendLine("Единица измерения не выбрана!");
            if (Product.Producer == null)
                stringBuilder.AppendLine("Производитель не выбрана!");
            if (Product.Supplier == null)
                stringBuilder.AppendLine("Поставщик не выбрана!");
            if (Product.MaxDiscount > 100 || Product.MaxDiscount < 0)
                stringBuilder.AppendLine("Неверная максимальная скидка!");
            if (Product.ActualDiscount > Product.MaxDiscount || Product.ActualDiscount < 0)
                stringBuilder.AppendLine("Неверная действующая скидка!");
            if (Product.Price < 0)
                stringBuilder.AppendLine("Неверная цена!");

            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            
            DataAccess.SaveProduct(Product);
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.DeleteProduct(Product);
            NavigationService.GoBack();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
