using System;
using System.Collections.Generic;
using Nancy.Security;

namespace BrettPit.Models
{
    public class UserModel : IUserIdentity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public Guid LoginGuid { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}