using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Data.Entities
{
    public class Task
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeLimit { get; set; }
        public double Budget { get; set; }
        public string StatusID { get; set; }
        public Status Status { get; set; }
        public string LocationID { get; set; }
        public Location Location { get; set; }
        public string CategoryID { get; set; }
        public Category Category { get; set; }
        public string Photo { get; set; }
    }
}
