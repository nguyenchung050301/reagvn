using e_commercial.Constants;
using e_commercial.DTOs.Request.User;
using e_commercial.DTOs.Response.Mail;
using e_commercial.DTOs.Response.User;
using e_commercial.Exceptions;
using e_commercial.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RazorLight;
using StackExchange.Redis;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace e_commercial.Services
{
    public class MailService : BackgroundService
    {
        private readonly RabbitMqConsumer _consumer;
        private readonly IDatabase _redis;
        public MailService(RabbitMqConsumer consumer, IConnectionMultiplexer muxer)
        {
            _consumer = consumer;
            _redis = muxer.GetDatabase();

        }

        private List<(string, ProductTypeEnum, float, int)> productDetails = new List<(string, ProductTypeEnum, float, int)>();

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Register
            _consumer.RegisterEventHandler<UserMailDTO>("register-mail-queue",
           SendMessageRegister);

            return Task.Delay(-1, stoppingToken);
        }



        public async Task SendOrderMailAsync(string userName, string Email, string orderId)
        {
            if (productDetails.Count == 0)
            {
                throw new BadValidationException("No products added to the order.");
            }

            //Model
            var model = new OrderMailDTO
            {
                UserName = userName,
                Email = Email,
                OrderId = orderId,
                ProductDetail = productDetails
            };

            //Engine 
            var engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates"))
                .UseMemoryCachingProvider()
                .Build();

            //Render 
            string body = await engine.CompileRenderAsync("OrderMail.cshtml", model);


            //Mail Sender
            using var message = new MailMessage("7up101101@gmail.com", model.Email)
            {
                Subject = "Đơn hàng của bạn tại ReagVN Shop",
                Body = body,
                IsBodyHtml = true
            };

            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("7up101101@gmail.com", "urhldlniwvhoqcmy"),
                EnableSsl = true
            };

            smtp.Send(message);
            productDetails.Clear(); // Clear the product details after sending the email
        }

        public async Task SendMessageRegister(UserMailDTO mailDTO)
        {
            var engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates"))
           .UseMemoryCachingProvider()
           .Build();

            // var mailDTO = new UserMailDTO
            // {
            //     Username = mailDTO.Username,
            //     UserEmail = mailDTO.UserEmail,
            // };

            //Render 
            string bodyRender = await engine.CompileRenderAsync("RegisterMail.cshtml", mailDTO);

            //Mail Sender
            using var message = new MailMessage("7up101101@gmail.com", "nhohacam@gmail.com")
            {
                Subject = "Xac Nhan Dang Ky",
                Body = bodyRender,
                IsBodyHtml = true
            };


            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("7up101101@gmail.com", "urhldlniwvhoqcmy"),
                EnableSsl = true
            };

            smtp.Send(message);

        }

        public void AddProduct(IEnumerable<(string, ProductTypeEnum, float, int)> products)
        {
            productDetails.AddRange(products);
        }


    }
}
