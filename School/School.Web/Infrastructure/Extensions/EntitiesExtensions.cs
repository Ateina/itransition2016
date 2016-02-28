using School.Entities;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateMaterial(this Material material, MaterialViewModel materialVm)
        {
            material.ID = new Random().Next();
            material.Title = materialVm.Title;
            material.Description = materialVm.Description;
            material.CategoryId = materialVm.CategoryId;
            material.Author = materialVm.Author;
            material.Content = materialVm.Content;
            material.Rating = 1;
            material.TrailerURI = null;
            material.Image = null;
            //material.Rating = materialVm.Rating;
            //material.TrailerURI = materialVm.TrailerURI;
            material.CreateDate = materialVm.CreateDate;
        }

        public static void UpdateUser(this User user, UserViewModel userVm)
        {
            user.Username = userVm.Username;
            user.City = userVm.City;
            user.DateOfBirth = userVm.DateOfBirth;
            user.Email = userVm.Email;
            user.RegistrationDate = (user.RegistrationDate == DateTime.MinValue ? DateTime.Now : userVm.RegistrationDate);
        }
    }
}