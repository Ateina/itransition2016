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
using School.Web.Infrastructure.Extensions;
using School.Data.Extensions;


namespace School.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<User> _usersRepository;

        public UserController(IEntityBaseRepository<User> usersRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _usersRepository = usersRepository;
        }

        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            filter = filter.ToLower().Trim();

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var users = _usersRepository.GetAll()
                    .Where(c => c.Email.ToLower().Contains(filter) ||
                    c.Username.ToLower().Contains(filter));

                var usersVm = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

                response = request.CreateResponse<IEnumerable<UserViewModel>>(HttpStatusCode.OK, usersVm);

                return response;
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var user = _usersRepository.GetSingle(id);

                UserViewModel userVm = Mapper.Map<User, UserViewModel>(user);

                response = request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userVm);

                return response;
            });
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request, UserViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                /*
                else
                {
                    if (_usersRepository.UserExists(user.Email))
                    {
                        ModelState.AddModelError("Invalid user", "Email or Identity Card number already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        User newUser = new User();
                        newUser.UpdateUser(user);
                        _usersRepository.Add(newUser);

                        _unitOfWork.Commit();

                        // Update view model
                        user = Mapper.Map<User, UserViewModel>(newUser);
                        response = request.CreateResponse<UserViewModel>(HttpStatusCode.Created, user);
                    }
                }
                */
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, UserViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    User _user = _usersRepository.GetSingle(user.ID);
                    _user.UpdateUser(user);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<User> users = null;
                int totalUsers = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    users = _usersRepository.FindBy(c => c.Username.ToLower().Contains(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalUsers = _usersRepository.GetAll()
                        .Where(c => c.Username.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    users = _usersRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalUsers = _usersRepository.GetAll().Count();
                }

                IEnumerable<UserViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

                PaginationSet<UserViewModel> pagedSet = new PaginationSet<UserViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalUsers,
                    TotalPages = (int)Math.Ceiling((decimal)totalUsers / currentPageSize),
                    Items = usersVM
                };

                response = request.CreateResponse<PaginationSet<UserViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
    }
}
