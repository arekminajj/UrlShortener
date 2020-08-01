using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string IdString { get; set; }
        public string RedirectUrl { get; set; }
    }
}
