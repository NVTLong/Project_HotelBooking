using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.Models.ViewModels
{
    public class AuthViewModel
    {
        public LoginViewModel Login { get; set; } = new LoginViewModel();
        public RegisterViewModel Register { get; set; } = new RegisterViewModel();
        public string ActiveMode { get; set; } = "login"; // "login" or "register"
    }
}
