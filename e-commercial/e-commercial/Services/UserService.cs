using e_commercial.Models;
using e_commercial.Repositories;
using System.Text.RegularExpressions;
using e_commercial.Exceptions;
using e_commercial.Constants;
using e_commercial.Repositories.Interfaces;
using e_commercial.DTOs.Request.User;
using AutoMapper;

using StackExchange.Redis;
using e_commercial.DTOs.Response.User;
using System;
using System.Text.Json;
namespace e_commercial.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDatabase _redis;
        private readonly RabbitMqProducer _producer;
        public UserService(IUserRepository userRepository, IMapper mapper, IConnectionMultiplexer muxer, RabbitMqProducer producer)
        {
            _redis = muxer.GetDatabase();
            _producer = producer;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task Register(UserCreateDTO userDTO)
        {
            //check register
            var existing = _userRepository.GetAll().FirstOrDefault(p => p.Username == userDTO.Username || p.UserEmail == userDTO.UserEmail || p.UserPhone == userDTO.UserPhone);
            if (existing != null)
            {
                if (existing.Username == userDTO.Username)
                    throw new BadValidationException("Username already exists");

                if (existing.UserEmail == userDTO.UserEmail)
                    throw new BadValidationException("Email already exists");

                if (existing.UserPhone == userDTO.UserPhone)
                    throw new BadValidationException("Phone already exists");
            }

            if (!IsPhoneNumber(userDTO.Username))
            {
                throw new BadValidationException("Not a valid phone number format.", nameof(userDTO.Username));
            }

            if (!IsEmail(userDTO.UserEmail))
            {
                throw new BadValidationException("Not a valid email format.", nameof(userDTO.UserEmail));
            }

            //
            /*  user.UserId = Guid.NewGuid().ToString();
              user.Username = userDTO.Username;
              string hashedPassword = hashPassword(userDTO.Userpassword);
              user.Userpassword = hashedPassword;
              user.UserRole = RoleEnum.User; // Default role
              user.UserShownname = userDTO.UserShownname;
              user.UserDistrict = userDTO.UserDistrict;
              user.UserWard = userDTO.UserWard;
              user.UserAddress = userDTO.UserAddress;
              user.UserPhone = user.Username;
              user.UserEmail = userDTO.UserEmail;*/

            var user = _mapper.Map<User>(userDTO);
            user.UserId = Guid.NewGuid().ToString();
            user.Userpassword = hashPassword(user.Userpassword);
            user.CreatedAt = DateTime.UtcNow;


            _userRepository.Add(user);
            var mailDTO = new UserMailDTO
             {
                 UserEmail = user.UserEmail,
              //   UserOTP = otp,
                 Username = user.Username,
             };
            await SendOtpToMail(mailDTO);

        }
        /// <summary>
        /// NOTE: Ko duoc de user spam gui mail 
        /// </summary>
        /// <param name="mailDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SendOtpToMail(UserMailDTO mailDTO)
        {
            var user = _userRepository.FindByEmail(mailDTO.UserEmail);
            mailDTO.UserOTP = RandomOTPGenerate();
            await _redis.MyStringSetAsync(user.UserId, mailDTO.UserOTP, 2000000000);

            await _producer.ProduceAsync("register-mail-queue", mailDTO);

        }


        /// <summary>
        /// Use case: user co mail xac nhan, nhan button (thay = duong link tren api)
        /// B1: kiem tra email co verify chua
        ///     Neu roi: log loi tra ve "mail da xac nhan roi" - Status code: 400
        ///     Neu chua: -> B2
        /// B2: kiem tra otp cua mail co dung trong redis hay ko
        ///     Tim thay: thi moi so sanh, neu dung -> B3, neu sai thi thong bao loi "otp ko dung hoac het han"
        ///     Ko tim thay: otp da het han -> API resend
        /// B3: cap nhat column isVerified trong table User trong database
        /// B4: tra status code: 204
        /// </summary>
        /// <param name="verificationDTO"></param>
        public void VerifyOTP(UserVerificationDTO verificationDTO)
        {
            var user = _userRepository.FindByEmail(verificationDTO.UserMail);
            if (user != null)
            {
                if (user.IsVerified == true)
                {
                    throw new BadValidationException("This mail is already verified");
                }

                string redisOtp = JsonSerializer.Deserialize<string>(_redis.StringGet(user.UserId));
                //if verified = false

                if (verificationDTO.UserOtp.Trim() != redisOtp) //stringget = otp value, identify by userId
                {
                    throw new BadValidationException("OTP is not matched or expired");
                }

                //if found
                user.IsVerified = true;
                _userRepository.Update(user);
            }

        }

        public User LoadByUserId(string userId)
        {
            //check login
            var check = _userRepository.GetAll().FirstOrDefault(p => p.UserId == userId);
            if (check == null)
            {
                throw new BadValidationException("Khong tim thay tai khoan", nameof(userId));
            }
            return check;
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
        private string RandomOTPGenerate()
        {
            var random = new Random();
            int num = random.Next(100000, 1000000);
            return num.ToString();
        }
    }
}
