using EmployeeManagement.Application.DTOs.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {

        public CreateEmployeeValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(1000);
        }

    }
}
