using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Purpose of this page is to create the database for our movies. 

namespace MovieCollection.Models
{
    public class MovieDbContext : DbContext
    {
        //Inheriting from the base class. 
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}
