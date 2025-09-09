using e_commercial.Constants;
using e_commercial.DTOs.Response.Mail;
using e_commercial.Exceptions;
using e_commercial.Models;
using RazorLight;
using System.Net;
using System.Net.Mail;

namespace e_commercial.Services
{
    public class MailService
    {
        private List<(string, ProductTypeEnum, float, int)> productDetails = new List<(string, ProductTypeEnum, float, int)>();
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

        public void AddProduct(IEnumerable<(string,ProductTypeEnum,float,int)> products)
        { 
            productDetails.AddRange(products);
        }
    }

}
