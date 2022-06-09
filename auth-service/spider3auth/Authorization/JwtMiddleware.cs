using Microsoft.Extensions.Options;
using spider3auth.Helpers;
using spider3auth.Services;

namespace spider3auth.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate requestDelegate, IOptions<AppSettings> appSettings)
        {
            _requestDelegate = requestDelegate;
            _appSettings = appSettings.Value;

        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId);
            }

            await _requestDelegate(context);
        }
    }
}
