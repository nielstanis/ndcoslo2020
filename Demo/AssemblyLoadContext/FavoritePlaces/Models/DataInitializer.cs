using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritePlaces.Models
{
    public static class DataInitializer
    {
        public static async void CreateDatabaseImportData(this PlacesDbContext context, string JSONData = "Data/Places.json")
        {
            _ = await context.Database.EnsureCreatedAsync();

            if (context.Places.Count() == 0)
            {
                if (System.IO.File.Exists(JSONData))
                {
                    var res = System.Text.Json.JsonSerializer.Deserialize(System.IO.File.ReadAllText(JSONData), typeof(List<Place>)) as List<Place>;
                    await context.Places.AddRangeAsync(res);
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
