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
using System.Windows.Shapes;
using Decor.Pages;

namespace Decor
{
    /// <summary>
    /// Interaction logic for DecorWindow.xaml
    /// </summary>
    public partial class DecorWindow : Window
    {
        public DecorWindow()
        {
            InitializeComponent();

            frame.NavigationService.Navigate(new AuthorizationPage());
            frame.Navigated += Frame_Navigated;
        }

        /// <summary>
        /// Метод для изменения данных в заголовке при навигации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var content = frame.Content as Page;

            tbTitle.Text = content.Title;

            tbFullName.Text = App.User == null ? "" : App.User.FullName;
        }

        /// <summary>
        /// Кнопка выхода из аккаунта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.User = null;
            frame.Navigate(new AuthorizationPage());
        }
    }
}
