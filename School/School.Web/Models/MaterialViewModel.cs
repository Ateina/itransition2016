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
    [Bind(Exclude = "Image")]
    public class MaterialViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }//содержание
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        public byte Rating { get; set; }
        public string TrailerURI { get; set; }
        public bool IsAvailable { get; set; }
        public int NumberOfTags { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new MaterialViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}