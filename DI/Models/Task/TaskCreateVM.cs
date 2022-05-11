using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.Task
{
    public class TaskCreateVM
    {
        private DateTime limitTime = DateTime.Now;

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LimitTime 
        {
            get
            {
                return this.limitTime;
            }
            set
            {
                this.limitTime = value;
            }
        }
        public double Budget { get; set; }
        public string Location { get; set; }
        public ICollection<Data.Entities.Location> Locations { get; set; }
        public string Category { get; set; }
        public ICollection<Data.Entities.Category> Categories { get; set; }
    }
}
