namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IPaymentService
    {
        Task PayAsync(int bookingId, string method);
    }
}
