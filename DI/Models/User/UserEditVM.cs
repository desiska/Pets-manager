using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.User
{
    public class UserEditVM : UserCreateVM
    {
        [Required]
        public string ID { get; set; }
    }
}
