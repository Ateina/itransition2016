using FluentValidation;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Infrastructure.Validators
{
    public class MaterialViewModelValidator : AbstractValidator<MaterialViewModel>
    {
        public MaterialViewModelValidator()
        {
            RuleFor(material => material.CategoryId).GreaterThan(0)
                .WithMessage("Select a Category");

            RuleFor(material => material.Content).NotEmpty().Length(1, 100)
                .WithMessage("Select a Content");

            RuleFor(material => material.Author).NotEmpty().Length(1, 50)
                .WithMessage("Select a author");

            RuleFor(movie => movie.Description).NotEmpty()
                .WithMessage("Select a description");

            RuleFor(movie => movie.Rating).InclusiveBetween((byte)0, (byte)5)
                .WithMessage("Rating must be less than or equal to 5");

            RuleFor(movie => movie.TrailerURI).NotEmpty().Must(ValidTrailerURI)
                .WithMessage("Only Youtube Trailers are supported");
        }

        private bool ValidTrailerURI(string trailerURI)
        {
            return (!string.IsNullOrEmpty(trailerURI) && trailerURI.ToLower().StartsWith("https://www.youtube.com/watch?"));
        }
    }
}