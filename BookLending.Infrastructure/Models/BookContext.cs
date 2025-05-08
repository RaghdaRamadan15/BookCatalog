using BookLending.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Models
{
    public class BookContext : IdentityDbContext
    {
        public DbSet<Book>books { get; set; }
        public DbSet<Borrowing> borrows { get; set; }
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
    }
}
