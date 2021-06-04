using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinksAPI.Models
{
    public class Drink
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alcohol { get; set; }
        public string Profile { get; set; }
        public string Image { get; set; }
    }
}
