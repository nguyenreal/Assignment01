using Hotel_BussinessObjects;
using Hotel_Services;
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

namespace FUMiniHotelManagement
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly IRoomTypeService roomTypeService ;
        private readonly IRoomService roomService ;
        private readonly IBookingService bookingService ;
        private readonly ICustomerService customerService ;
        private readonly IBookingReservastionService bookingReservastionService ;
        private List<RoomInformation> availableRooms = new();
        private readonly int customerId;

        public CustomerWindow(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            this.roomService = new RoomService();
            this.roomTypeService = new RoomTypeService();
            this.bookingService = new BookingService();
            this.customerService = new CustomerService();
            this.bookingReservastionService = new BookingReservationService();

            LoadRoomTypes();
            LoadBookingDetails();
        }
        private void LoadRoomTypes()
        {
            var roomTypes = roomTypeService.GetRoomTypes();
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeId";
        }

        private void LoadRoomsByType(int roomTypeId)
        {
            availableRooms = roomService.GetRoomInformations()
                .Where(r => r.RoomTypeId == roomTypeId && r.RoomStatus == 1).ToList();
            cboRoom.ItemsSource = availableRooms;
            cboRoom.DisplayMemberPath = "RoomNumber";
            cboRoom.SelectedValuePath = "RoomId";
        }
        private void LoadBookingDetails()
        {
            try
            {
                var bookingList = bookingService.GetBookingDetails();
                dgData.ItemsSource = bookingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");
            }
            finally
            {
                ClearForm();
            }
        }

        private void CalculateTotalPrice()
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue && cboRoom.SelectedValue is int roomId)
            {
                var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == roomId);
                if (selectedRoom?.RoomPricePerDay.HasValue == true)
                {
                    decimal totalPrice = (dpEndDate.SelectedDate.Value - dpStartDate.SelectedDate.Value).Days * selectedRoom.RoomPricePerDay.Value;
                    txtTotalPrice.Text = totalPrice.ToString("F2"); // Ensures no currency symbol
                }
            }
        }

        private void cboRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboRoomType.SelectedValue is int roomTypeId)
            {
                LoadRoomsByType(roomTypeId);
            }
        }

        private void cboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboRoom.SelectedValue is int roomId)
            {
                var selectedRoom = availableRooms.FirstOrDefault(r => r.RoomId == roomId);
                if (selectedRoom != null)
                {
                    txtPricePerDay.Text = selectedRoom.RoomPricePerDay?.ToString("C") ?? "N/A";
                    CalculateTotalPrice();
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate != null && dpEndDate.SelectedDate != null)
            {
                if (dpEndDate.SelectedDate <= dpStartDate.SelectedDate)
                {
                    dpEndDate.SelectedDate = dpStartDate.SelectedDate?.AddDays(1);
                }
                CalculateTotalPrice();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem == null) return;

            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedIndex < 0) return;

            // Get the selected row and check if it exists
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null) return;

            // Get the cell content and check if it's valid
            DataGridCell rowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
            if (rowColumn?.Content is not TextBlock textBlock || string.IsNullOrWhiteSpace(textBlock.Text)) return;

            // Attempt to parse the ID if valid
            if (!int.TryParse(textBlock.Text, out int bookingId)) return;

            BookingDetail booking = bookingService.GetBookingDetailById(bookingId);

            // Điền thông tin trên data Grid
            dpStartDate.Text = booking.StartDate.ToString();
            dpEndDate.Text = booking.EndDate.ToString();
            cboRoom.SelectedValue = booking.RoomId;
            txtTotalPrice.Text = booking.ActualPrice?.ToString("F2") ?? "0.00";
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set thông tin Booking Reservation
                var bookingReservation = new BookingReservation
                {
                    BookingReservationId = int.Parse(txtBookingID.Text),
                    CustomerId = customerId,
                    BookingDate = DateOnly.FromDateTime(DateTime.Now), // Set the booking date to the current date
                    TotalPrice = 0, // We'll calculate this later, or you can set an initial value
                    BookingDetails = new List<BookingDetail>() // Initialize the list for booking details
                };

                // Tạo Booking Reservation
                var bookingReservationCreated = bookingReservastionService.CreateBookingReservation(bookingReservation);
                
                if (!bookingReservationCreated)
                {
                    MessageBox.Show("Failed to create booking reservation. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; 
                }

                // Set thông tin Booking Details
                var bookingDetail = new BookingDetail
                {
                    BookingReservationId = bookingReservation.BookingReservationId, // Set the foreign key
                    RoomId = (int)cboRoom.SelectedValue,
                    StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value),
                    ActualPrice = decimal.TryParse(txtTotalPrice.Text, out decimal totalPrice) ? totalPrice : (decimal?)null
                };

                // Tạo Booking Detail
                var bookingDetailCreated = bookingService.CreateBookingDetail(bookingDetail);
                if (!bookingDetailCreated)
                {
                    MessageBox.Show("Failed to create booking detail. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; 
                }

                // Cập nhật totalPrice
                bookingReservation.TotalPrice += bookingDetail.ActualPrice ?? 0; 


                MessageBox.Show("Booking added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadBookingDetails(); 
                ClearForm(); 
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearForm()
        {
            txtBookingID.Text = "";
            dpStartDate.Text = "";
            dpEndDate.Text = "";
            cboRoom.SelectedValue = null;
            txtTotalPrice.Text = "";
        }

        private void txtBookingID_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
