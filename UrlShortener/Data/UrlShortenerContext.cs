using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext (DbContextOptions<UrlShortenerContext> options)
            : base(options)
        {
        }

        public DbSet<UrlShortener.Models.Url> Url { get; set; }
    }
}
