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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>

    public class FiilingList
    {
        public Color ListItemBackground;
        public TextDecorationCollection Decorations;
        public string DiscountPrice;
        public Visibility IsVisible;
    }

    public partial class UserWindow : Window
    {
        public static List<Product> productsInBasket = new List<Product>();
        public static UserWindow userWindow1;
        public List<Product> mainProducts = new List<Product>();
        public List<Product> products = new List<Product>();
        public List<Product> commonList = new List<Product>();
        public List<FiilingList> fiilingLists = new List<FiilingList>();
        public int allProducts;
        public int nowInList;

        public UserWindow()
        {
            InitializeComponent();
            FillListOfProducts();
            userWindow1 = this;
        }

        private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ContextMenu contextMenu = new ContextMenu();
            MenuItem addToOrderMenuItem = new MenuItem();
            addToOrderMenuItem.Header = "Добавить к заказу";
            addToOrderMenuItem.Click += AddToOrderMenuItem_Click;
            contextMenu.Items.Add(addToOrderMenuItem);
            myList.ContextMenu = contextMenu;
        }

        private void AddToOrderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)myList.SelectedItem;
            productsInBasket.Add(selectedProduct);
            if (productsInBasket.Count > 0)
            {
                basket.Visibility = Visibility.Visible;
            }
        }
        private void Busket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.Basket userWindow = new Windows.Basket();
                userWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка перехода в окно корзины!");
            }
        }

        public void FillListOfProducts()
        {
            try
            {
                mainProducts = new List<Product>();
                products = new List<Product>();
                commonList = new List<Product>();
                fiilingLists = new List<FiilingList>();
                allProducts = 0;
                nowInList = 0;
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                foreach (Product product in db.Product)
                {
                    FiilingList fiilingList = new FiilingList();
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
                    mainProducts.Add(product);
                    commonList.Add(product);
                    products.Add(product);
                    fiilingLists.Add(fiilingList);
                    allProducts++;
                }
                myList.ItemsSource = products;
                nowInList = myList.Items.Count;
                Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        private void DiscountsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<Product> discountsProducts = new List<Product>();
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                foreach (Product product in products)
                {
                    if (Cathegory.SelectedIndex == 0)
                    {
                        discountsProducts.Add(product);
                    }
                    else if (Cathegory.SelectedIndex == 1)
                    {
                        if ((float)product.ProductDiscountAmount > (float)0 && (float)product.ProductDiscountAmount < (float)9.99)
                        {
                            discountsProducts.Add(product);
                        }
                    }
                    else if (Cathegory.SelectedIndex == 2)
                    {
                        if ((float)product.ProductDiscountAmount >= (float)10 && (float)product.ProductDiscountAmount < (float)14.99)
                        {
                            discountsProducts.Add(product);
                        }
                    }
                    else if (Cathegory.SelectedIndex == 3)
                    {
                        if ((float)product.ProductDiscountAmount >= (float)15)
                        {
                            discountsProducts.Add(product);
                        }
                    }
                }
                commonList = products.Intersect(discountsProducts).ToList();
                myList.ItemsSource = commonList;
                SortingComboBox_SelectionChanged(sender, null);
                nowInList = myList.Items.Count;
                Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        public static List<Product> WithoutSort(List<Product> products)
        {
            return products.OrderBy(p => p.ProductID).ToList();
        }

        public static List<Product> SortAscending(List<Product> products)
        {
            return products.OrderBy(p => p.ProductCost).ToList();
        }

        public static List<Product> SortDescending(List<Product> products)
        {
            return products.OrderByDescending(p => p.ProductCost).ToList();
        }


        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SortingBy.SelectedIndex == 0)
                {
                    List<Product> withotSort = WithoutSort(commonList);
                    myList.ItemsSource = withotSort;
                    nowInList = myList.Items.Count;
                    Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
                }
                else if (SortingBy.SelectedIndex == 1)
                {
                    List<Product> sortedAscending = SortAscending(commonList);
                    myList.ItemsSource = sortedAscending;
                    nowInList = myList.Items.Count;
                    Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
                }
                else if (SortingBy.SelectedIndex == 2)
                {
                    List<Product> sortedDescending = SortDescending(commonList);
                    myList.ItemsSource = sortedDescending;
                    nowInList = myList.Items.Count;
                    Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != null && Search.Text != "")
            {
                List<Product> searchProducts = mainProducts.FindAll(x => x.ProductName.Contains(Search.Text));
                products = searchProducts;
                myList.ItemsSource = searchProducts;
                nowInList = myList.Items.Count;
                Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
            }
            else
            {
                products = mainProducts;
                myList.ItemsSource = mainProducts;
                nowInList = myList.Items.Count;
                Quantity.Content = nowInList.ToString() + " из " + allProducts.ToString();
            }
            Cathegory.SelectedIndex = 0;
            SortingBy.SelectedIndex = 0;
        }
    }
}
