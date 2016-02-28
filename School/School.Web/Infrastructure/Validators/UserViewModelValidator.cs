using FluentValidation;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Infrastructure.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.Username).NotEmpty()
                .Length(1, 100).WithMessage("Name must be between 1 - 100 characters");

            RuleFor(user => user.DateOfBirth).NotNull()
                .LessThan(DateTime.Now.AddYears(-0))
                .WithMessage("User must be at least 0 years old.");

            RuleFor(user => user.Email).NotEmpty().EmailAddress()
                .WithMessage("Enter a valid Email address");
        }
    }
}