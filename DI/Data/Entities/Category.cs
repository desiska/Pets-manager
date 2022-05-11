using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Data.Entities
{
    public class Category
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public ICollection<Data.Entities.Task> Tasks { get; set; }
    }
}
