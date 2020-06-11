using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritePlaces.Models
{
    public class Place
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string FriendlyText => Text.Length > 20 ? $"{Text.Substring(0, 20)}..." : Text;
    }
}
