using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Uppgift3.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Artist> Artist { get; set; }
    public DbSet<Borrower> Borrower { get; set; }
    public DbSet<CD> CD { get; set; }
    public DbSet<Loan> Loan { get; set; }
}
