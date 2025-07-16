using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;

namespace BlazorWebApp.Data
{
    public class BlazorWebAppContext : DbContext
    {
        public BlazorWebAppContext (DbContextOptions<BlazorWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<BlazorApp1.Models.Movie> Movie { get; set; } = default!;
    }
}
