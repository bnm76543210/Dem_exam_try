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
using iTextSharp.text;
using System.IO;
using System.Windows.Shapes;
using Paragraph = iTextSharp.text.Paragraph;

namespace Preddiplom_practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        public List<FiilingList> fiilingLists = new List<FiilingList>();
        public List<OrderProduct> orderProducts = new List<OrderProduct>();
        public Basket()
        {
            InitializeComponent();
            FillListOfProducts();
            if(MainWindow.userMain != null)
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
                    MessageBox.Show("Marat tried");
                    db.Order.Add(order);
                    db.SaveChanges();
                    foreach (Product product in UserWindow.productsInBasket)
                    {
                        OrderProduct productOrder = new OrderProduct();                   
                        productOrder.OrderID = order.OrderID;
                        productOrder.ProductID = product.ProductID;
                        productOrder.Count = UserWindow.productsInBasket.Count(x => x == product);
                        if(productOrder.Count > 1)
                        {
                            UserWindow.productsInBasket.Distinct().Where(x => x == product);
                        }
                        db.OrderProduct.Add(productOrder);
                    }
                    db.SaveChanges();
                    Application app = new Application();
                    Document document = new Document();
                    PdfWriter.GetInstance(document, new FileStream("example.pdf", FileMode.Create));
                    document.Open();

                    // Добавление текста в PDF файл
                    Paragraph paragraph = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    Paragraph paragraph1 = new Paragraph("Дата заказа: " + order.OrderCreateDate + "\n");
                    document.Add(paragraph1);
                    // Закрытие PDF файла
                    document.Close();
                    Document doc = app.Documents.Add();
                    doc.Content.Text += "Дата заказа: " + order.OrderCreateDate + "\n" +
                                       "Номер заказа: " + order.OrderID + "\n" +
                                       "Состав заказа: " + "\n" + GetProductsInOrderString(str) + "\n" +
                                       "Сумма заказа: " + TotalSumOfOrder + "\n" +
                                       "Сумма скидки: " + TotalSumOfOrderWithDiscounts + "\n" +
                                       "Пункт выдачи: " + UserProductWindow.currentOrder.PickupPoint.Address + "\n" +
                                       "Код получения: " + UserProductWindow.currentOrder.OrderGetCode + "\n";
                    doc.SaveAs(@"B:\Dem_exam_try-master\check.pdf", WdSaveFormat.wdFormatPDF);
                    Process.Start(@"B:\Dem_exam_try-master\check.pdf");
                    doc.Close();
                    app.Quit();
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
