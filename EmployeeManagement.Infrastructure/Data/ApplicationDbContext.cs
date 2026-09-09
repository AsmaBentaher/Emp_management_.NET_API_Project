using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Domain.Entities;
//Gives access to the parten "DbContext" class.
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data;

// :DbContext ==> "ApplciationDbContext inherits from DbContext class.
// DbContext: Gives class the database powers.
public class ApplicationDbContext : DbContext
{
    //The object of this class will hold the configuration settings of the database, such as the connection string, the db provider.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
    }

    //DbSet<Employee>() ==> Represents the Employees table in the DB.
    // => set<Employee>() ==> Grab the reference for the Employee table.
    public DbSet<Employee> Employees => Set<Employee>();

}
