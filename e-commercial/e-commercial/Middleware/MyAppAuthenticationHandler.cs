using e_commercial.Controllers;
using e_commercial.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace e_commercial.Middleware
{
    public class MyAppAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly JWTService _jWTService;

        public MyAppAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock,
            JWTService jWTService)
            : base(options, logger, encoder, clock)
        {
            _jWTService = jWTService;
        }
        /// <summary>
        /// b1: lay token tu header Authorization
        /// b2: kiem tra token co hop le hay khong
        /// b3: neu co bat dau tu khoa 'Bearer ', neu co thi cat chu 'Bearer ' di
        /// b4: verify token bang jwtSecurityTokenHandler
        /// b5: goi ham getclaim co tham so la token
        /// b6: tao authorization ticket voi cac claim da lay duoc tu token
        /// </summary>
        /// <returns></returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {          
            var getTokenHandler = _jWTService.GetJwtSecurityTokenHandler();
            _jWTService.GetJwtSecurityToken(Context.Request.Headers["Authorization"]);
            // Implement your authentication logic here
            Console.WriteLine("MyAppAuthenticationHandler: HandleAuthenticateAsync called");
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
    }
  
}
