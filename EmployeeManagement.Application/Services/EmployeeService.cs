using System;
using System.Collections.Generic;
using System.Text;

using EmployeeManagement.Application.DTOs.Employee;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService
    {
        //Private variable to hold the repository(Cannot be changed to something else once it's set).
        private readonly IEmployeeRepository _repository;

        //Constructor method.
        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        //Fetches all employees, asks the repository for the data in an asynchronous wait.
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }


        // Accepts a DTO then use the data to construct a new employee.
        public async Task<Employee> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                //Generate a new unique primary key.
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary
            };

            return await _repository.AddAsync(employee);
        }

        
        // Tells the api controller whether the update succeded or failed.
        public async Task<bool> UpdateAsync(Guid id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return false;

            // Update the emp data using the DTO data.
            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            await _repository.UpdateAsync(employee);

            return true;
        }



        // Deletes the employees and tells the api controller whether the deletion failed or succeeded.
        public async Task<bool> DeleteAsync(Guid id)
        {
            var emp = await _repository.GetByIdAsync(id);

            if (emp == null)
                return false;

            await _repository.DeleteAsync(emp);

            return true;
        }
        
    }
}
