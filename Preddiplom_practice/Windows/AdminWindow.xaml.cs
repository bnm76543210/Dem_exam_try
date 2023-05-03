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

namespace Preddiplom_practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            //FillListOfProducts();
        }
        //private void FillListOfProducts()
        //{
        //    try
        //    {
        //        Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
        //        foreach (Product product in db.Product)
        //        {
        //            if (product.ProductPhoto == null)
        //            {
        //                product.ProductPhoto = "/Preddiplom_practice;/Pictures/picture.png";
        //            }
        //            else
        //            {
        //                product.ProductPhoto = "/Preddiplom_practice;/Pictures/" + product.ProductPhoto;
        //            }
        //            products.Add(product);
        //        }
        //        myList.ItemsSource = products;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
        //    }
        //}
    }
}
