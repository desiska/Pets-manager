using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Data.Entities
{
    public class Location
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Data.Entities.Task> Tasks { get; set; }

    }
}
