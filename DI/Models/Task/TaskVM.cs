﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.Task
{
    public class TaskVM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LimitTime { get; set; }
        public double Budget { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public ICollection<Data.Entities.Status> Statuses { get; set; }
    }
}
