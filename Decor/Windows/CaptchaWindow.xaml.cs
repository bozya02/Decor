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

namespace Decor.Windows
{
    /// <summary>
    /// Interaction logic for CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        public CaptchaWindow()
        {
            InitializeComponent();

            var letters = "QWERTYUIOPasdfghjkl1234567890";

            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                tbCaptcha.Text += $"{letters[rnd.Next(0, letters.Length - 1)]}";
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = tbEnter.Text == tbCaptcha.Text;
        }
    }
}
