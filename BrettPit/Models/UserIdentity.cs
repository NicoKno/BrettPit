using System.Collections.Generic;

namespace BrettPit.Models
{
    public class UserIdentity : Nancy.Security.IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}