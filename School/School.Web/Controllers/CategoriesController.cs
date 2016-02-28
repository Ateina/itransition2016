using AutoMapper;
using School.Data.Infrastructure;
using School.Data.Repositories;
using School.Entities;
using School.Web.Infrastructure.Core;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace School.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Category> _categoriesRepository;

        public CategoriesController(IEntityBaseRepository<Category> categoriesRepository,
             IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _categoriesRepository = categoriesRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var categories = _categoriesRepository.GetAll().ToList();

                IEnumerable<CategoryViewModel> categoriesVM = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

                response = request.CreateResponse<IEnumerable<CategoryViewModel>>(HttpStatusCode.OK, categoriesVM);

                return response;
            });
        }
    }
}