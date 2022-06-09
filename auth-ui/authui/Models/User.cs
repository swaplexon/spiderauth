using spider3auth.Common;

namespace authui.Models
{
    public class User
    {
        public String Id { get; set; }
        public string UserName { get; set; }

        public UserTypeEnum UserType { get; set; }
        public string JwtToken { get; set; }


        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
        //public bool IsDeleting { get; set; }
    }
}
