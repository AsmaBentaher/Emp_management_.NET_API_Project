using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(Guid id);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<Employee> AddAsync(Employee employee);
    }
}
