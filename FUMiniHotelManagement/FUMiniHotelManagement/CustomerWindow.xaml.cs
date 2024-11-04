using Hotel_BussinessObjects;
using Hotel_Services;
using System;
using System.Collections;
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
    public partial class CustomerWindow : Window
    {
        private readonly IRoomTypeService roomTypeService;
        private readonly IRoomService roomService;
        private readonly IBookingService bookingService;
        private readonly ICustomerService customerService;
        private readonly IBookingReservastionService bookingReservastionService;
        private ArrayList availableRooms;
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
            this.availableRooms = new ArrayList();

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
            availableRooms = new ArrayList();
            var allRooms = roomService.GetRoomInformations();
            foreach (RoomInformation room in allRooms)
            {
                if (room.RoomTypeId == roomTypeId && room.RoomStatus == 1)
                {
                    availableRooms.Add(room);
                }
            }
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
            // Kiểm tra ngày đặt phòng
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue && cboRoom.SelectedValue is int roomId)
            {
                RoomInformation selectedRoom = null;
                //Lấy ra phòng được chọn
                foreach (RoomInformation room in availableRooms)
                {
                    if (room.RoomId == roomId)
                    {
                        selectedRoom = room;
                        break;
                    }
                }

                // Kiểm tra phòng có bị null và có giá không
                if (selectedRoom != null && selectedRoom.RoomPricePerDay != null)
                {
                    //Tính tiền
                    decimal totalPrice = (dpEndDate.SelectedDate.Value - dpStartDate.SelectedDate.Value).Days * selectedRoom.RoomPricePerDay.Value;
                    txtTotalPrice.Text = totalPrice.ToString("F2");
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
            //Kiểm tra ID có tồn tại
            if (cboRoom.SelectedValue is int roomId)
            {
                //Lấy phòng được chọn từ trong arraylist phognf ra
                RoomInformation selectedRoom = null;
                foreach (RoomInformation room in availableRooms)
                {
                    if (room.RoomId == roomId)
                    {
                        selectedRoom = room;
                        break;
                    }
                }

                // Update price sau khi tìm được phòng
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

            // Lấy hàng được chọn và kiểm tra có null không
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null) return;

            // Lấy thông tin từ hàng và check valid
            DataGridCell rowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
            if (rowColumn?.Content is not TextBlock textBlock || string.IsNullOrWhiteSpace(textBlock.Text)) return;

            // parse id nếu valid
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
                // Set thông tin cho Booking Reservation
                var bookingReservation = new BookingReservation
                {
                    BookingReservationId = int.Parse(txtBookingID.Text),
                    CustomerId = customerId,
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    TotalPrice = 0,
                    BookingDetails = new ArrayList() 
                };

                // Call xuống service để save Booking Reservation
                var bookingReservationCreated = bookingReservastionService.CreateBookingReservation(bookingReservation);

                if (!bookingReservationCreated)
                {
                    MessageBox.Show("Failed to create booking reservation. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Set thông tin cho Booking Detail
                var bookingDetail = new BookingDetail
                {
                    BookingReservationId = bookingReservation.BookingReservationId,
                    RoomId = (int)cboRoom.SelectedValue,
                    StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value),
                    ActualPrice = decimal.TryParse(txtTotalPrice.Text, out decimal totalPrice) ? totalPrice : (decimal?)null
                };

                // Call xuống service để save Booking Detail
                var bookingDetailCreated = bookingService.CreateBookingDetail(bookingDetail);
                if (!bookingDetailCreated)
                {
                    MessageBox.Show("Failed to create booking detail. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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