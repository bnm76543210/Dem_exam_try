using iTextSharp.text.pdf;
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
using System.IO;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;
using Window = System.Windows.Window;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Preddiplom_practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>

    public partial class Basket : Window
    {
        public List<FiilingList> fiilingLists = new List<FiilingList>();
        public List<OrderProduct> orderProducts = new List<OrderProduct>();
        public decimal allSum = 0;
        public decimal discountSum = 0;
        public Basket()
        {
            InitializeComponent();
            FillListOfProducts();
            if (MainWindow.userMain != null)
            {
                UserLabel.Content = MainWindow.userMain.UserSurname + " " + MainWindow.userMain.UserName + " " + MainWindow.userMain.UserPatronymic;
            }
            else
            {
                UserLabel.Content = "";
            }
            try
            {
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                foreach (PickupPoint pickupPoint in db.PickupPoint)
                {
                    PointOfIssue.Items.Add(pickupPoint.Address);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PointOfIssue.SelectedItem != null)
                {
                    string str = "";
                    Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                    Order order = new Order();
                    order.OrderStatusID = 1;
                    order.PickupPointID = PointOfIssue.SelectedIndex;
                    order.OrderCreateDate = DateTime.Now;
                    if (UserWindow.productsInBasket.Count > 3)
                    {
                        order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                    }
                    else
                    {
                        order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                    }
                    if (MainWindow.userMain != null)
                    {
                        order.UserID = MainWindow.userMain.UserID;
                    }
                    else
                    {
                        order.UserID = null;
                    }
                    Random random = new Random();
                    order.OrderGetCode = random.Next(100, 1000);
                    db.Order.Add(order);
                    db.SaveChanges();
                    foreach (Product product in UserWindow.productsInBasket)
                    {
                        OrderProduct productOrder = new OrderProduct();
                        productOrder.OrderID = order.OrderID;
                        productOrder.ProductID = product.ProductID;
                        productOrder.Count = UserWindow.productsInBasket.Count(x => x == product);
                        orderProducts.Add(productOrder);
                    }
                    orderProducts = orderProducts.GroupBy(x => x.ProductID).Select(x => x.First()).ToList();
                    foreach (OrderProduct productOrder in orderProducts)
                    {
                        db.OrderProduct.Add(productOrder);
                    }
                    db.SaveChanges();
                    str = "Дата заказа: " + order.OrderCreateDate + "\n" +
                          "Номер заказа: " + order.OrderID + "\n" +
                          "Состав заказа: " + "\n" + GetProductsInOrderString("") + "\n" +
                          "Сумма заказа: " + allSum + "\n" +
                          "Сумма скидки: " + discountSum + "\n" +
                          "Пункт выдачи: " + PointOfIssue.Text + "\n" +
                          "Код получения: " + order.OrderGetCode + "\n";
                    Application wordApp = new Application();
                    Document wordDoc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Paragraph para = wordDoc.Content.Paragraphs.Add();
                    para.Range.Text = str;
                    para.Range.Font.Name = "Arial";
                    para.Range.Font.Size = 12;
                    para.Range.InsertParagraphAfter();
                    string pdfFilePath = @"F:\Order.pdf";
                    wordDoc.SaveAs2(pdfFilePath, WdExportFormat.wdExportFormatPDF);
                    UserWindow.productsInBasket.Clear();
                    UserWindow.userWindow1.basket.Visibility = Visibility.Hidden;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Выберите пункт выдачи!");
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }
        public string GetProductsInOrderString(string str)
        {
            decimal totalSum = 0;
            decimal totalDiscountSum = 0;
            foreach (Product product in UserWindow.productsInBasket)
            {
                totalSum += product.ProductCost * UserWindow.productsInBasket.Count;
                totalDiscountSum +=
                    (decimal)(product.ProductCost * product.ProductDiscountAmount / 100 * UserWindow.productsInBasket.Count);
                str += "Артикул: " + product.ProductArticleNumber + "\n";
                str += "Наименование: " + product.ProductName + "\n";
                str += "Единица измерения: " + product.UnitType.UnitTypeName + "\n";
                str += "Цена: " + (product.ProductCost * UserWindow.productsInBasket.Count) + "\n";
                str += "Поставщик: " + product.ProductSupplier.ProductSupplierName + "\n";
                str += "Категория: " + product.ProductCategory.ProductCategoryName + "\n";
                str += "Скидка: " + product.ProductDiscountAmount + "%\n";
                str += "Описание: " + product.ProductDescription + "\n";
                str += "\n";
            }
            allSum = totalSum;
            discountSum = totalDiscountSum;
            return str;
        }

        private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ContextMenu contextMenu = new ContextMenu();
            MenuItem addToOrderMenuItem = new MenuItem();
            addToOrderMenuItem.Header = "Удалить из корзины";
            addToOrderMenuItem.Click += RemoveFromOrderItem_Click;
            contextMenu.Items.Add(addToOrderMenuItem);
            myList.ContextMenu = contextMenu;
        }

        private void RemoveFromOrderItem_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)myList.SelectedItem;
            UserWindow.productsInBasket.Remove(selectedProduct);
            if (UserWindow.productsInBasket.Count > 0)
            {
                UserWindow.userWindow1.basket.Visibility = Visibility.Visible;
            }
            else
            {
                UserWindow.userWindow1.basket.Visibility = Visibility.Hidden;
                this.Close();
            }
            FillListOfProducts();
        }

        public void FillListOfProducts()
        {
            try
            {
                myList.Items.Refresh();
                foreach (Product product in UserWindow.productsInBasket)
                {
                    FiilingList fiilingList = new FiilingList();
                    if (product.ProductPhoto != null)
                    {
                        product.ProductPhoto = product.ProductPhoto.Replace("/Resources/", "");
                        product.ProductPhoto = product.ProductPhoto.Replace("/Resources/picture.png", "");
                    }
                    if (product.ProductPhoto == null || product.ProductPhoto == "")
                    {
                        product.ProductPhoto = "/Resources/picture.png";
                    }
                    else
                    {
                        product.ProductPhoto = "/Resources/" + product.ProductPhoto;
                    }
                    if (product.ProductDiscountAmount > 0)
                    {
                        fiilingList.ListItemBackground = Color.FromRgb(127, 255, 0);
                        fiilingList.Decorations = TextDecorations.Strikethrough;
                        fiilingList.DiscountPrice = (product.ProductCost - ((product.ProductCost / 100) * product.ProductDiscountAmount)).ToString();
                        fiilingList.IsVisible = Visibility.Visible;
                    }
                    else
                    {
                        fiilingList.ListItemBackground = Color.FromRgb(255, 255, 255);
                        fiilingList.IsVisible = Visibility.Hidden;
                    }
                    fiilingLists.Add(fiilingList);
                }
                myList.ItemsSource = UserWindow.productsInBasket;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }
    }
}
