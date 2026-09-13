using EmployeeManagement.Application.DTOs.Employee;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Application.Validators;
using EmployeeManagement.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        private readonly IValidator<CreateEmployeeDto> _createValidator;
        private readonly IValidator<UpdateEmployeeDto> _updateValidator;

        public EmployeeController(
            IEmployeeService service,
            IValidator<CreateEmployeeDto> createValidator,
            IValidator<UpdateEmployeeDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

            [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            
            var employee = await _service.GetAllAsync();

            var empDto =  employee.Select(employee => new CreateEmployeeDto
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
            }).ToList();

            return Ok(new
            {
                message = "Employees retrieved successfully",
                data = empDto
            });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound(new
                {
                    message = $"Employee with ID '{id}' was not found.",
                    data = employee
                });

            return Ok(new
            {
                message = $"Employee retrieved successfully.",
               data = employee
            }
);
        }


            [HttpPost]
            public async Task<IActionResult> Create(CreateEmployeeDto dto)
            {
                var validationResult = await _createValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = validationResult.Errors.Select(e => new
                        {
                            field = e.PropertyName,
                            message = e.ErrorMessage
                        })
                    });
                }

                var employee = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = employee.Id },
                    new
                    {
                        message = "Employee created successfully.",
                        data = employee
                    }
                );
            }

            [HttpPost("{id:guid}")]
            public async Task<IActionResult> Update(Guid id, UpdateEmployeeDto dto)
            {
                var validationResult = await _updateValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = validationResult.Errors.Select(e => new
                        {
                            field = e.PropertyName,
                            message = e.ErrorMessage
                        })
                    });
                }

                var updated = await _service.UpdateAsync(id, dto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        message = $"Employee with ID '{id}' was not found.",
                        data = updated
                    });
                }

                return Ok(new
                {
                    message = "Employee updated successfully.",
                    data = updated
                });
            }

            [HttpDelete("{id:guid}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var deleted = await _service.DeleteAsync(id);

                if (!deleted)
                    return NotFound(new
                    {
                        message = $"Employee with ID '{id}' was not found.",
                        data = deleted
                    });

                return Ok(new
                {
                    message = $"Employee deleted successfully."
                });
        }
    }
}
