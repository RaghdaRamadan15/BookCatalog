using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookLending.Domain.Models
{
    public static class SettingAdmin
    {
        public const string Email = "admin@gmail.com";
        public const string UserName = "admin11";
        public const string Password = "Admin@123";
        public const string Role = "Admin";
    }
}
