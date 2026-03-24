using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task PayAsync(int bookingId, string method)
        {
            var payment = await _paymentRepository.GetByBookingIdAsync(bookingId);

            if (payment == null)
                throw new Exception("Không tìm thấy payment");

            if (payment.Status == PaymentStatus.Paid)
                throw new Exception("Đã thanh toán rồi");

            payment.Status = PaymentStatus.Paid;
            payment.PaymentMethod = method;
            payment.PaidAt = DateTime.Now;
            payment.TransactionCode = Guid.NewGuid().ToString();

            await _paymentRepository.UpdateAsync(payment);
            await _paymentRepository.SaveAsync();
        }
    }

}
