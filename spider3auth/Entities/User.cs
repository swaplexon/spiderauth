using System.Text.Json.Serialization;
using System.Collections.Generic;
using spider3auth.Repository;
using spider3auth.Common;

namespace spider3auth.Entities
{
    [BsonCollection("useritem")]
    public class User : Document
    {

        public string CustomerAccountID { get; set; }


        public string Username { get; set; }


        public string Password { get; set; }


        public string Description { get; set; }


        public bool IsActive { get; set; }


        public UserTypeEnum UserType { get; set; }

        public IList<GroupItemCollection> GroupItemList { get; set; }
        public object RunTimeCustomerAccountItem { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}