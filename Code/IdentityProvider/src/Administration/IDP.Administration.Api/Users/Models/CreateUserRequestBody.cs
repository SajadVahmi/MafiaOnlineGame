using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Users.Models
{
    public class CreateUserRequestBody
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? LockoutEnabled { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public bool? OtpSmsEnabled { get; set; }
    }
}
