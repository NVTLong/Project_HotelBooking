using AutoMapper;
using BCrypt.Net;
using NuGet.Protocol.Core.Types;
using Project_HotelBooking.Application.DTOs.Customer;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;
using Project_HotelBooking.Repository;

namespace Project_HotelBooking.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return _mapper.Map<CustomerDto?>(customer);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task CreateAsync(CustomerCreateDto dto, int userIdCreate)
        {
            var customer = _mapper.Map<Customer>(dto);

            if (!string.IsNullOrEmpty(dto.Password))
            {
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _customerRepository.CreateAsync(customer);
        }

        public async Task UpdateAsync(CustomerUpdateDto dto, int userId)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.Id);

            if (customer == null)
                throw new Exception("Customer not found");

            _mapper.Map(dto, customer);

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var roomType = await _customerRepository.GetByIdAsync(id);
            if (roomType != null)
            {
                await _customerRepository.DeleteAsync(id);
                await _customerRepository.SaveAsync();
            }
        }

        public async Task<CustomerDto> RegisterAsync(CustomerRegisterDto dto)
        {
            var exist = await _customerRepository.GetByEmailAsync(dto.Email);

            if (exist != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            var customer = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsEmailVerified = false,
                LastLoginAt = DateTime.UtcNow,
            };

            await _customerRepository.CreateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> LoginAsync(CustomerLoginDto dto)
        {
            var customer = await _customerRepository.GetByEmailAsync(dto.Email);

            if (customer == null)
                throw new Exception("Email không tồn tại");

            if (string.IsNullOrEmpty(customer.PasswordHash))
                throw new Exception("Tài khoản chưa có mật khẩu");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash);

            if (!isValid)
                throw new Exception("Sai mật khẩu");

            customer.LastLoginAt = DateTime.Now;

            await _customerRepository.UpdateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
