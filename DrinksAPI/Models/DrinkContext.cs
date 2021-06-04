using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DrinksAPI.Models
{
    public class DrinkContext : DbContext
    {
        public DrinkContext(DbContextOptions<DrinkContext> options)
            : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
    }
}
