using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Domain.Entities
{
    public class Destination
    {
        public int Id { get; set; }             
        public string Name { get; set; }        
        public string ImageUrl { get; set; }    
        public string Description { get; set; } 
        public decimal? AverageCost { get; set; }
    }

}
