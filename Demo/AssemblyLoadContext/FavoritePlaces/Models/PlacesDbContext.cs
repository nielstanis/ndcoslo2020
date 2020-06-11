using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritePlaces.Models
{
    public class PlacesDbContext : DbContext
    {
        public DbSet<Place> Places
        {
            get;set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify the path of the database here
            optionsBuilder.UseSqlite("Filename=./Data/places.sqlite");
        }
    }


}
