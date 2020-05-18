
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Entities
{
    public class User : IEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public bool IsActivatedMailSend { get; set; }
    }
}
