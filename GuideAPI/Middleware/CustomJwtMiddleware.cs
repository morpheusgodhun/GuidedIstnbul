using Core.IService;
using Service.Security.JWT;

namespace GuideAPI.Middleware
{
    public class CustomJwtMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IConfiguration configuration)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token is not null && !string.IsNullOrWhiteSpace(token))
            {
                var claims = JwtHelper.ValidateToken(token, configuration.GetSection("TokenOptions")["SecurityKey"]);
                context.User = claims;
            }

            await _next(context);
        }
    }
}
