using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // A private unchangeable variable that holds the live connection windwo to the database.
        private readonly ApplicationDbContext _Context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _Context = context;
        }


        // _.Context.Employees --> Targets the employee table in the database.
        // .ToListAsync() --> An entity framework method that tells the database to run a (SELECT * FROM Employees) query,pulls all the rows into a C# list, and do it asynchronously.
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _Context.Employees.ToListAsync();
        }



        // Searches through the employee table and find the targeted employee with the matching id.
        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            //The FirstAsync method does not accept raw id parameter.
            //that's why we should use Lambda expression.
            return await _Context.Employees.FirstAsync(e => e.Id == id);
        }


        // Places the new employee object into Entity Framework's trackig memory.
        // Then forces the the EF Core to compile an explicit (INSERT Into Employee ..).
        // Then returns the saved employee object back to the apllication Layer.
        public async Task<Employee> AddAsync(Employee employee)
        {
            await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();

            return employee;
        }


        // The EF core examines the employee object, track the changed fields.
        // then generates an sql query to update the targeted employee.
        public async Task UpdateAsync(Employee employee)
        {
            _Context.Employees.Update(employee);
            await _Context.SaveChangesAsync();
        }


        // flags the specific tracking entity for deletion.
        // Then generates and runs an SQL query to delete the targeted employee.
        public async Task DeleteAsync(Employee employee)
        {
            _Context.Employees.Remove(employee);
            await _Context.SaveChangesAsync();
        }
    }
}
