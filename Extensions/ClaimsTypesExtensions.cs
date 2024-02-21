using System.Security.Claims;

namespace JwtAspNet.Extensions
{
    public static class ClaimsTypesExtensions
    {
        public static int Id(this ClaimsPrincipal user)
        {
            try
            {
                var id = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? "0";
                return int.Parse(id);
            }
            catch
            {
                return 0;
            }
        }

        public static string Name(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Email(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GivenName(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Image(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == "image").Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}