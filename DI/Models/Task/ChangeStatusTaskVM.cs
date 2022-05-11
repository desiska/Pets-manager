using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.Task
{
    public class ChangeStatusTaskVM
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public ICollection<Data.Entities.Status> Statuses { get; set; }
        public IFormFile Photo { get; set; }
    }
}
