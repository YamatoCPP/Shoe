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

namespace Project3
{
    /// <summary>
    /// Логика взаимодействия для CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : Page
    {
        public CaptchaPage()
        {
            InitializeComponent();

            GenCaptcha();

            CaptchaTB.Visibility = Visibility.Hidden;
            CaptchaEnt.Visibility = Visibility.Hidden;
        }

        private void GenCaptcha()
        {

            const String Symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random _random = new Random();
            String captcha = new String(Enumerable.Repeat(Symbols, 4).Select(s => s[_random.Next(s.Length)]).ToArray());

            CaptchaTB.Text = captcha;
        }
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.role = RoleEnum.GUEST;
            Manager.Name = "Гость";
            Manager.MainFrame.Navigate(new ProductPage());
            CaptchaEnt.Visibility = Visibility.Hidden;
            CaptchaTB.Visibility = Visibility.Hidden;
            CaptchaEnt.Text = "";
            LoginBox.Text = "";
            PasswordBox.Text = "";
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length == 0 || PasswordBox.Text.Length == 0)
            {
                MessageBox.Show("Есть пустые поля!");
                return;
            }

            var clients = GayfullinTradeEntities.GetContext().User.Where(p => p.UserLogin == LoginBox.Text && p.UserPassword == PasswordBox.Text).ToList();
            if (CaptchaEnt.Visibility == Visibility.Visible)
            {
                if (CaptchaEnt.Text != CaptchaTB.Text)
                {
                    MessageBox.Show("Неверная каптча!");
                    GenCaptcha();
                    LoginButton.IsEnabled = false;
                    await Task.Delay(10000);
                    LoginButton.IsEnabled = true;
                    return;
                }
            }

            if (clients.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
                CaptchaTB.Visibility = Visibility.Visible;
                CaptchaEnt.Visibility = Visibility.Visible;
                return;
            }

            Manager.role = (RoleEnum)clients[0].Role.RoleID;
            Manager.Name = clients[0].UserName.ToString() + " " + clients[0].UserSurname.ToString() + " " + clients[0].UserPatronymic.ToString();
            Manager.MainFrame.Navigate(new ProductPage());
            CaptchaEnt.Visibility = Visibility.Hidden;
            CaptchaTB.Visibility = Visibility.Hidden;
            CaptchaEnt.Text = "";
            LoginBox.Text = "";
            PasswordBox.Text = "";
        }
    }
}
