using MongoDB.Bson;
using spider3auth.Common;
using spider3auth.Entities;
using System.Text.Json.Serialization;

namespace spider3auth.Models.Users
{
    public class AuthenticateResponse
    {
        public String Id { get; set; }
        public string UserName { get; set; }

        public UserTypeEnum UserType { get; set; }
        public string JwtToken { get; set; }


        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id.ToString();
            UserName = user.Username;
            UserType = user.UserType;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
