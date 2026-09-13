using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Application.DTOs.Employee;

namespace EmployeeManagement.Application.Validators
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeValidator()
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
                .GreaterThan(0);
        }
    }
}
