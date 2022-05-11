using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.Location
{
    public class LocationIndexVM
    {
        public ICollection<LocationVM> Items { get; set; }
    }
}
