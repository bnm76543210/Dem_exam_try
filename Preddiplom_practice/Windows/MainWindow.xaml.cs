using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Preddiplom_practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User userMain;
        DispatcherTimer _timer;
        TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = 0;
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                foreach (User user in db.User)
                {
                    if (user.UserLogin == TextBox1.Text && user.UserPassword == TextBox2.Text)
                    {
                        userMain = user;
                        foreach (Role role in db.Role)
                        {
                            if (user.RoleID == role.RoleID && role.RoleName == "Администратор")
                            {
                                a = 1;
                                MessageBox.Show("Вы вошли как администратор");
                                Windows.AdminWindow adminWindow = new Windows.AdminWindow();
                                this.Close();
                                adminWindow.ShowDialog();
                            }
                            if (user.RoleID == role.RoleID && role.RoleName == "Менеджер")
                            {
                                a = 1;
                                MessageBox.Show("Вы вошли как менеджер");
                                Windows.ManagerWindow menegerWindow = new Windows.ManagerWindow();
                                this.Close();
                                menegerWindow.ShowDialog();
                            }
                            if (user.RoleID == role.RoleID && role.RoleName == "Клиент")
                            {
                                a = 1;
                                MessageBox.Show("Вы вошли как клиент");
                                Windows.UserWindow userWindow = new Windows.UserWindow();
                                this.Close();
                                userWindow.ShowDialog();
                            }
                        }
                    }
                    if (user.UserLogin == TextBox1.Text && TextBox2.Text != user.UserPassword)
                    {
                        a = 1;
                        MessageBox.Show("Пароль не совпадает");
                        Windows.CAPTCHA cAPTCHA = new Windows.CAPTCHA();
                        cAPTCHA.ShowDialog();
                    }
                }
                if (a == 0)
                {
                    MessageBox.Show("Пользователь не найден");
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        public void StopAuthoristion(int timeToCount)
        {
            Welcome.Text = "Доступ заблокирован на " + timeToCount + " секунд";
            Welcome.Foreground = Brushes.Red;
            TextBox1.IsEnabled = false;
            TextBox2.IsEnabled = false;
            EnterAsGuest.IsEnabled = false;
            button1.IsEnabled = false;
            _time = TimeSpan.FromSeconds(timeToCount);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Welcome.Text = "Доступ заблокирован на " + _time.Seconds.ToString() + " секунд";
                if (_time == TimeSpan.Zero)
                {
                    Welcome.Foreground = Brushes.Black;
                    TextBox1.IsEnabled = true;
                    TextBox2.IsEnabled = true;
                    button1.IsEnabled = true;
                    EnterAsGuest.IsEnabled = true;
                    Welcome.Text = "ДОБРО ПОЖАЛОВАТЬ!";
                    _timer.Stop();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Windows.UserWindow userWindow = new Windows.UserWindow();
                this.Close();
                userWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка перехода в окно пользователя!");
            }
        }
    }
}

