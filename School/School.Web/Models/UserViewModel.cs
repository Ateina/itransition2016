using School.Web.Infrastructure.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Web.Models
{
    public class UserViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hobby { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext
       validationContext)
        {
            var validator = new UserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage,
           new[] { item.PropertyName }));
        }
    }
}