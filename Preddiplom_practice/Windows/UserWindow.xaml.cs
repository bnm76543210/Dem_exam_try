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

        //public FiilingList(Product product)
        //{
        //    this.ProductID = product.ProductID;
        //    this.ProductArticleNumber = product.ProductArticleNumber;
        //    this.ProductName = product.ProductName;
        //    this.UnitTypeID = product.UnitTypeID;
        //    this.ProductCost = product.ProductCost;
        //    this.ProductMaxDiscountAmount = product.ProductMaxDiscountAmount;
        //    this.ProductManufacturerID = product.ProductManufacturerID;
        //    this.ProductSupplierID = product.ProductSupplierID;
        //    this.ProductCategoryID = product.ProductCategoryID;
        //    this.ProductDiscountAmount = product.ProductDiscountAmount;
        //    this.ProductQuantityInStock = product.ProductQuantityInStock;
        //    this.ProductDescription = product.ProductDescription;
        //    this.ProductPhoto = product.ProductPhoto;
        //}
    }

    public partial class UserWindow : Window
    {
        public List<Product> products = new List<Product>();
        public List<FiilingList> fiilingLists = new List<FiilingList>();

        public UserWindow()
        {
            InitializeComponent();
            FillListOfProducts();           
        }
        private void FillListOfProducts()
        {
            try
            {
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                foreach (Product product in db.Product)
                {
                    FiilingList fiilingList = new FiilingList();
                    //fiilingList = (FiilingList)product;
                    if (product.ProductPhoto == null || product.ProductPhoto == "")
                    {
                        product.ProductPhoto = "/Resources/picture.png";
                    }
                    else
                    {
                        product.ProductPhoto = "/Resources/" + product.ProductPhoto;
                    }
                    //fiilingList.ProductManufacturer = product.ProductManufacturer;
                    //fiilingList.ProductName = product.ProductName;
                    //fiilingList.ProductDescription = product.ProductDescription;
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
                    products.Add(product);
                    fiilingLists.Add(fiilingList);
                }
                //myList.ItemsSource = fiilingLists;
                myList.ItemsSource = products;
                //foreach (var item in myList.Items)
                //{
                //    TextBlock textBlock = (TextBlock)FindName("ManufacturerLabel");
                //    if (textBlock != null)
                //    {
                //        MessageBox.Show(textBlock.Text);
                //    }
                //}
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
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                myList.ItemsSource = null;
                products.Clear();
                foreach (Product product in db.Product)
                {
                    if (product.ProductPhoto == null || product.ProductPhoto == "")
                    {
                        product.ProductPhoto = "/Resources/picture.png";
                    }
                    else
                    {
                        product.ProductPhoto = "/Resources/" + product.ProductPhoto;
                    }
                    if (Cathegory.SelectedIndex == 0)
                    {
                        products.Add(product);
                    }
                    else if(Cathegory.SelectedIndex == 1)
                    {
                        if((float)product.ProductDiscountAmount > (float)0 && (float)product.ProductDiscountAmount < (float)9.99)
                        {
                            products.Add(product);
                        }
                    }
                    else if (Cathegory.SelectedIndex == 2)
                    {
                        if ((float)product.ProductDiscountAmount >= (float)10 && (float)product.ProductDiscountAmount < (float)14.99)
                        {
                            products.Add(product);
                        }
                    }
                    else if (Cathegory.SelectedIndex == 3)
                    {
                        if ((float)product.ProductDiscountAmount >= (float)15)
                        {
                            products.Add(product);
                        }
                    }
                }
                myList.ItemsSource = products;
                SortingComboBox_SelectionChanged(sender, null);
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
                    List<Product> withotSort = WithoutSort(products);
                    myList.ItemsSource = withotSort;
                }
                else if (SortingBy.SelectedIndex == 1)
                {
                    List<Product> sortedAscending = SortAscending(products);
                    myList.ItemsSource = sortedAscending;
                }
                else if (SortingBy.SelectedIndex == 2)
                {
                    List<Product> sortedDescending = SortDescending(products);
                    myList.ItemsSource = sortedDescending;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> searchProducts = products.FindAll(x => x.ProductName.Contains(Search.Text));
            myList.ItemsSource = searchProducts;
            //DiscountsComboBox_SelectionChanged(sender, null);
            //SortingComboBox_SelectionChanged(sender, null);
        }
    }
}
