using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для RedactingProduct.xaml
    /// </summary>
    public partial class RedactingProduct : Window
    {
        public RedactingProduct()
        {
            InitializeComponent();
            AddData(AdminWindow.product1);
        }

        private void AddData(Product mainproduct)
        {
            if (mainproduct != null)
            {
                try
                {
                    Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                    deleteBTN.Visibility = Visibility.Visible;
                    addredBTN.Content = "Редактировать";
                    Article.IsEnabled = false;
                    Article.Text = mainproduct.ProductArticleNumber;
                    ProductName.Text = mainproduct.ProductName;
                    foreach (ProductCategory productCategory in db.ProductCategory)
                    {
                        Cathegory.Items.Add(productCategory.ProductCategoryName);
                        if (mainproduct.ProductCategoryID == productCategory.ProductCategoryID)
                        {
                            Cathegory.SelectedItem = productCategory.ProductCategoryName;
                        }
                    }
                    Price.Text = mainproduct.ProductCost.ToString();
                    foreach (UnitType unitType in db.UnitType)
                    {
                        UnitType.Items.Add(unitType.UnitTypeName);
                        if (mainproduct.UnitTypeID == unitType.UnitTypeID)
                        {
                            UnitType.SelectedItem = unitType.UnitTypeName;
                        }
                    }
                    QuantityInStock.Text = mainproduct.ProductQuantityInStock.ToString();
                    foreach (ProductManufacturer productManufacturer in db.ProductManufacturer)
                    {
                        Manufcturer.Items.Add(productManufacturer.ProductManufacturerName);
                        if (mainproduct.ProductManufacturerID == productManufacturer.ProductManufacturerID)
                        {
                            Manufcturer.SelectedItem = productManufacturer.ProductManufacturerName;
                        }
                    }
                    foreach (ProductSupplier productSupplier in db.ProductSupplier)
                    {
                        Supplier.Items.Add(productSupplier.ProductSupplierName);
                        if (mainproduct.ProductSupplierID == productSupplier.ProductSupplierID)
                        {
                            Supplier.SelectedItem = productSupplier.ProductSupplierName;
                        }
                    }
                    MaxDiscount.Text = mainproduct.ProductMaxDiscountAmount.ToString();
                    NowDiscount.Text = mainproduct.ProductDiscountAmount.ToString();
                    Description.Text = mainproduct.ProductDescription;
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка прогрузки данных для редактирования товара!");
                }
            }
            else
            {
                try
                {
                    Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                    foreach (ProductCategory productCategory in db.ProductCategory)
                    {
                        Cathegory.Items.Add(productCategory.ProductCategoryName);
                    }
                    foreach (UnitType unitType in db.UnitType)
                    {
                        UnitType.Items.Add(unitType.UnitTypeName);
                    }
                    foreach (ProductManufacturer productManufacturer in db.ProductManufacturer)
                    {
                        Manufcturer.Items.Add(productManufacturer.ProductManufacturerName);
                    }
                    foreach (ProductSupplier productSupplier in db.ProductSupplier)
                    {
                        Supplier.Items.Add(productSupplier.ProductSupplierName);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка прогрузки данных для добавления товара!");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = 0;
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                MessageBox.Show(AdminWindow.product1.ProductName);
                foreach (Product product in db.Product)
                {
                    if (product.ProductID == AdminWindow.product1.ProductID)
                    {
                        db.Product.Remove(product);
                        i++;
                        MessageBox.Show(i.ToString());
                    }
                }
                db.SaveChanges();
                if (i == 0)
                {
                    MessageBox.Show("Продукт не удалён");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Продукт удалён");
                    AdminWindow.adminWindow.myList.SelectedItem = null;
                    AdminWindow.adminWindow.FillListOfProducts();
                    this.Close();                    
                }
        }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
}

        private void AddRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addredBTN.Content.ToString() == "Редактировать")
            {
                int i = 0;
                float costData;
                int quantityData;
                int quantityData1;
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                if (ProductName.Text != null && ProductName.Text != "")
                {
                    AdminWindow.product1.ProductName = ProductName.Text;
                    i++;
                }
                if (Price.Text != null && Price.Text != "" && float.TryParse(Price.Text, out costData) == true)
                {
                    if (costData > 0)
                    {
                        AdminWindow.product1.ProductCost = Convert.ToDecimal(costData);
                        i++;
                    }
                }
                if (QuantityInStock.Text != null && QuantityInStock.Text != "" && int.TryParse(QuantityInStock.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        AdminWindow.product1.ProductQuantityInStock = Convert.ToInt16(quantityData);
                        i++;
                    }
                }
                if (MaxDiscount.Text != null && MaxDiscount.Text != "" && int.TryParse(MaxDiscount.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        AdminWindow.product1.ProductMaxDiscountAmount = Convert.ToByte(quantityData);
                        i++;
                    }
                }
                if (NowDiscount.Text != null && NowDiscount.Text != "" && int.TryParse(NowDiscount.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        AdminWindow.product1.ProductDiscountAmount = Convert.ToByte(quantityData);
                        i++;

                    }
                }
                if (int.TryParse(MaxDiscount.Text, out quantityData) == true && int.TryParse(NowDiscount.Text, out quantityData1) == true)
                {
                    if (quantityData > 0 && quantityData1 > 0 && quantityData1 <= quantityData)
                    {
                        i++;
                    }
                    else
                    {
                        MessageBox.Show("Скидка больше максимально заложенной");
                    }
                }
                if (Description.Text != null && Description.Text != "")
                {
                    AdminWindow.product1.ProductDescription = Description.Text;
                    i++;
                }
                if (Cathegory.SelectedItem != null)
                {
                    foreach (ProductCategory productCategory in db.ProductCategory)
                    {
                        if (Cathegory.SelectedItem.ToString() == productCategory.ProductCategoryName)
                        {
                            AdminWindow.product1.ProductCategoryID = productCategory.ProductCategoryID;
                            i++;
                        }
                    }
                }
                if (UnitType.SelectedItem != null)
                {
                    foreach (UnitType unitType in db.UnitType)
                    {
                        if (UnitType.SelectedItem.ToString() == unitType.UnitTypeName)
                        {
                            AdminWindow.product1.UnitTypeID = unitType.UnitTypeID;
                            i++;
                        }
                    }
                }
                if (Manufcturer.SelectedItem != null)
                {
                    foreach (ProductManufacturer productManufacturer in db.ProductManufacturer)
                    {
                        if (Manufcturer.SelectedItem.ToString() == productManufacturer.ProductManufacturerName)
                        {
                            AdminWindow.product1.ProductManufacturerID = productManufacturer.ProductManufacturerID;
                            i++;
                        }
                    }
                }
                if (Supplier.SelectedItem != null)
                {
                    foreach (ProductSupplier productSupplier in db.ProductSupplier)
                    {
                        if (Supplier.SelectedItem.ToString() == productSupplier.ProductSupplierName)
                        {
                            AdminWindow.product1.ProductSupplierID = productSupplier.ProductSupplierID;
                            i++;
                        }
                    }
                }
                if (AdminWindow.product1.ProductPhoto != null)
                {
                    AdminWindow.product1.ProductPhoto = AdminWindow.product1.ProductPhoto.Replace("/Resources/", "");
                    AdminWindow.product1.ProductPhoto = AdminWindow.product1.ProductPhoto.Replace("/Resources/picture.png", "");
                }
                if (i == 11)
                {
                    db.Product.AddOrUpdate(AdminWindow.product1);
                    db.SaveChanges();
                    MessageBox.Show("Данные обновлены!");
                    AdminWindow.adminWindow.myList.SelectedItem = null;
                    AdminWindow.adminWindow.FillListOfProducts();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных!");
                    this.Close();
                }
            }
            else
            {
                int i = 0;
                float costData;
                int quantityData;
                int quantityData1;
                Preddiplom_practiceEntities db = new Preddiplom_practiceEntities();
                Product product = new Product();
                if (Article.Text != null && Article.Text != "")
                {
                    product.ProductArticleNumber = Article.Text;
                    i++;
                }
                if (ProductName.Text != null && ProductName.Text != "")
                {
                    product.ProductName = ProductName.Text;
                    i++;
                }
                if (Price.Text != null && Price.Text != "" && float.TryParse(Price.Text, out costData) == true)
                {
                    if (costData > 0)
                    {
                        product.ProductCost = Convert.ToDecimal(costData);
                        i++;
                    }
                }
                if (QuantityInStock.Text != null && QuantityInStock.Text != "" && int.TryParse(QuantityInStock.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        product.ProductQuantityInStock = Convert.ToInt16(quantityData);
                        i++;
                    }
                }
                if (MaxDiscount.Text != null && MaxDiscount.Text != "" && int.TryParse(MaxDiscount.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        product.ProductMaxDiscountAmount = Convert.ToByte(quantityData);
                        i++;
                    }
                }
                if (NowDiscount.Text != null && NowDiscount.Text != "" && int.TryParse(NowDiscount.Text, out quantityData) == true)
                {
                    if (quantityData > 0)
                    {
                        product.ProductDiscountAmount = Convert.ToByte(quantityData);
                        i++;
                    }
                }
                if (int.TryParse(MaxDiscount.Text, out quantityData) == true && int.TryParse(NowDiscount.Text, out quantityData1) == true)
                {
                    if (quantityData > 0 && quantityData1 > 0 && quantityData1 <= quantityData)
                    {
                        i++;
                    }
                    else
                    {
                        MessageBox.Show("Скидка больше максимально заложенной");
                    }
                }
                if (Description.Text != null && Description.Text != "")
                {
                    product.ProductDescription = Description.Text;
                    i++;
                }
                if (Cathegory.SelectedItem != null)
                {
                    foreach (ProductCategory productCategory in db.ProductCategory)
                    {
                        if (Cathegory.SelectedItem.ToString() == productCategory.ProductCategoryName)
                        {
                            product.ProductCategoryID = productCategory.ProductCategoryID;
                            i++;
                        }
                    }
                }
                if (UnitType.SelectedItem != null)
                {
                    foreach (UnitType unitType in db.UnitType)
                    {
                        if (UnitType.SelectedItem.ToString() == unitType.UnitTypeName)
                        {
                            product.UnitTypeID = unitType.UnitTypeID;
                            i++;
                        }
                    }
                }
                if (Manufcturer.SelectedItem != null)
                {
                    foreach (ProductManufacturer productManufacturer in db.ProductManufacturer)
                    {
                        if (Manufcturer.SelectedItem.ToString() == productManufacturer.ProductManufacturerName)
                        {
                            product.ProductManufacturerID = productManufacturer.ProductManufacturerID;
                            i++;
                        }
                    }
                }
                if (Supplier.SelectedItem != null)
                {
                    foreach (ProductSupplier productSupplier in db.ProductSupplier)
                    {
                        if (Supplier.SelectedItem.ToString() == productSupplier.ProductSupplierName)
                        {
                            product.ProductSupplierID = productSupplier.ProductSupplierID;
                            i++;
                        }
                    }
                }
                if (product.ProductPhoto != null) 
                {
                    product.ProductPhoto = product.ProductPhoto.Replace("/Resources/", "");
                    product.ProductPhoto = product.ProductPhoto.Replace("/Resources/picture.png", "");
                }
                if (i == 12)
                {
                    db.Product.Add(product);
                    db.SaveChanges();
                    MessageBox.Show("Данные добавлены!");
                    AdminWindow.adminWindow.myList.SelectedItem = null;
                    AdminWindow.adminWindow.FillListOfProducts();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении данных!");
                    this.Close();
                }
            }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка связи с базой данных, исправьте ошибку!");
            }
        }
    }
}
