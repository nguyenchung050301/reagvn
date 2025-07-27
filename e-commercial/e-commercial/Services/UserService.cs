using e_commercial.Models;
using e_commercial.Repositories;
using System.Text.RegularExpressions;
using e_commercial.Exceptions;
using e_commercial.Constants;
using e_commercial.Repositories.Interfaces;
using e_commercial.DTOs.Request.User;
namespace e_commercial.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Register(UserCreateDTO userDTO)
        {
            //check register
            var existing = _userRepository.GetAll().FirstOrDefault(p => p.Username == userDTO.Username);
            if (existing != null)
            {
                throw new InvalidOperationException($"Username {userDTO.Username} already exists.");
            }
            User user = new User();
            if (!IsPhoneNumber(userDTO.Username))
            {
               throw new BadValidationException("Not a valid phone number format.", nameof(userDTO.Username));
            }
            if (!IsEmail(userDTO.UserEmail))
            {
                throw new BadValidationException("Not a valid email format.", nameof(userDTO.UserEmail));
            }
            //
            user.UserId = Guid.NewGuid().ToString();
            user.Username = userDTO.Username;
            string hashedPassword = hashPassword(userDTO.Userpassword);
            user.Userpassword = hashedPassword;
            user.UserRole = RoleEnum.User; // Default role
            user.UserShownname = userDTO.UserShownname;
            user.UserDistrict = userDTO.UserDistrict;
            user.UserWard = userDTO.UserWard;
            user.UserAddress = userDTO.UserAddress;
            user.UserPhone = user.Username;
            user.UserEmail = userDTO.UserEmail;
            _userRepository.Add(user); 
        }
        public User LoadByUserName(UserLoginDTO userDTO)
        {
            //check login
            var check = _userRepository.GetAll().FirstOrDefault(p => p.Username == userDTO.Username);
            if (check == null)
            {
                throw new BadValidationException("Nhap sai tai khoan hoac mat khau", nameof(userDTO.Username));
            }
            if (!BCrypt.Net.BCrypt.Verify(userDTO.Userpassword, check.Userpassword))
            {
                throw new BadValidationException("Nhap sai tai khoan hoac mat khau", nameof(userDTO.Userpassword));
            }
            return check;
        }
        private bool IsPhoneNumber(string num)
        {
            string pattern = "^(\\+84|0)[\\s\\-\\.]?\\(?\\d{1,4}\\)?[\\s\\-\\.]?\\d{3,4}[\\s\\-\\.]?\\d{3,4}$";
            
            return Regex.IsMatch(num, pattern);
        }
        private bool IsEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        private string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
