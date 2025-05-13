using ApiTaskManagement.Services.Interfaces;

namespace ApiTaskManagement.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            UserId = user?.FindFirst("user_id")?.Value
                     ?? throw new UnauthorizedAccessException("No se pudo obtener el user_id del token.");
        }
    }
}
