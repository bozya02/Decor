using Decor.DB;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private int _attempt = 0;

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = pbPassword.Password;

            if (_attempt != 0 && !(bool)(new Windows.CaptchaWindow().ShowDialog()))
                return;
            

            if ((App.User = DataAccess.Login(login, password)) != null)
            {
                NavigationService.Navigate(new ProductsListPage());
            }
            else
            {
                _attempt++;
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsListPage());
        }
    }
}
