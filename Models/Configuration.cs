using System.Collections.Generic;

namespace API
{
    public class Configuration
    {
        private Dictionary<string, string> connectionStrings;

        public Configuration()
        {
            connectionStrings = new Dictionary<string, string>();
            connectionStrings.Add("TodoStoreDb", "mongodb://localhost:27017");
        }

        public string GetConnectionString(string name)
        {
            return connectionStrings[name];
        }
    }
}