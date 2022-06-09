using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spider3auth.Repository
{
    public interface IMongoSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
    public class MongoSettings : IMongoSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
