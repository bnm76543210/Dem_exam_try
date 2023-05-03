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
using System.Windows.Threading;

namespace Preddiplom_practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для CAPTCHA.xaml
    /// </summary>
    public partial class CAPTCHA : Window
    {
        private string randomText;
        private int randomCharsCount;
        public CAPTCHA()
        {
            InitializeComponent();
            GenerateRandomText();
        }

        private void GenerateRandomText()
        {
            Random random = new Random();
            randomText = "";
            randomCharsCount = random.Next(4, 6);
            for (int i = 0; i < randomCharsCount; i++)
            {
                int charType = random.Next(2);
                if (charType == 0)
                {
                    randomText += (char)('0' + random.Next(10));
                }
                else
                {
                    randomText += (char)('A' + random.Next(26));
                }
            }
            Captcha.Content = randomText;
        }

        private void captchaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (captchaTextBox.Text == Captcha.Content.ToString())
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Капча была введена неверно. Доступ заблокирован на 10 секунд");
                    this.Close();
                    MainWindow mainWindow = (Preddiplom_practice.MainWindow)App.Current.MainWindow;
                    mainWindow.StopAuthoristion(10);
                }
            }
        }
    }
}
