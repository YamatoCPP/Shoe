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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private int total;
        private List<Product> currentProducts;
        
        public ProductPage()
        {
            InitializeComponent();
            currentProducts = GayfullinTradeEntities.GetContext().Product.ToList();
            discountFiltration.SelectedIndex = costSort.SelectedIndex = 0;
            total = currentProducts.Count;
            Update();
            SetRecordCount(currentProducts.Count);

            AuthNameTB.Text = "Вы авторизованы как: " + Manager.Name;
            switch (Manager.role)
            {
                case RoleEnum.CLIENT:
                    RoleTB.Text = "Роль: клиент";
                    break;
                case RoleEnum.MANAGER:
                    RoleTB.Text = "Роль: менеджер";
                    break;
                case RoleEnum.ADMIN:
                    RoleTB.Text = "Роль: админ";
                    break;
                case RoleEnum.GUEST:
                    RoleTB.Text = "Роль: гость";
                    break;
            }

        }

        private void SetRecordCount(int count)
        {
            recordsCountBox.Text = count.ToString() + "/" + total.ToString();
        }

        private void Update()
        {
            currentProducts = GayfullinTradeEntities.GetContext().Product.ToList();
            UpdateFiltration();
            UpdateSort();
            UpdateSearch();
            ProductView.ItemsSource = currentProducts.ToList();
            SetRecordCount(currentProducts.Count);
        }
        private void UpdateFiltration()
        {
            switch (discountFiltration.SelectedIndex)
            {
                case 1:
                    currentProducts = currentProducts.Where(p => p.ProductDiscountAmount > 0 && p.ProductDiscountAmount < 10).ToList();
                    break;
                case 2:
                    currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15).ToList();
                    break;
                case 3:
                    currentProducts = currentProducts.Where(p => p.ProductDiscountAmount >= 15).ToList();
                    break;
            }
           
        }

        private void UpdateSort()
        {
            switch (costSort.SelectedIndex)
            {
                case 1:
                    currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList();
                    break;
                case 2:
                    currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
                    break;
            }
        }

        private void UpdateSearch()
        {
            currentProducts = currentProducts.Where(p => p.ProductName.Contains(nameSearch.Text)).ToList();
        }

        private void discountFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void costSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void nameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
