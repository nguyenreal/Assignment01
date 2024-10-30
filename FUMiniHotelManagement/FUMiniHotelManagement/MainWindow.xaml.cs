using Hotel_BussinessObjects;
using Hotel_Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FUMiniHotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICustomerService iCustomerService;
        public MainWindow()
        {
            InitializeComponent();
            iCustomerService = new CustomerService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Customer account = iCustomerService.GetCustomerByEmail(txtEmail.Text);
            if (account != null && account.Password.Equals(txtPassword.Password))
            {
                this.Hide();
                CustomerWindow customerWindow = new CustomerWindow(account.CustomerId);
                customerWindow.Show();
            }
            else
            {
                MessageBox.Show("You're not permitted !");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}