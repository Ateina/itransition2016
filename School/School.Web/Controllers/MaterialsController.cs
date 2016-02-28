using AutoMapper;
using School.Data.Infrastructure;
using School.Data.Repositories;
using School.Entities;
using School.Web.Infrastructure.Core;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using School.Web.Infrastructure.Extensions;
using School.Data.Extensions;

namespace School.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/materials")]
    public class MaterialsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Material> _materialsRepository;

        public MaterialsController(IEntityBaseRepository<Material> materialsRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork) : base(_errorsRepository, _unitOfWork)
        {
            _materialsRepository = materialsRepository;
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var materials = _materialsRepository.GetAll().OrderByDescending(m => m.CreateDate).Take(6).ToList();

                IEnumerable<MaterialViewModel> materialsVM = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(materials);

                response = request.CreateResponse<IEnumerable<MaterialViewModel>>(HttpStatusCode.OK, materialsVM);

                return response;
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var material = _materialsRepository.GetSingle(id);

                MaterialViewModel materialVM = Mapper.Map<Material, MaterialViewModel>(material);

                response = request.CreateResponse<MaterialViewModel>(HttpStatusCode.OK, materialVM);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Material> materials = null;
                int totalMaterials = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    materials = _materialsRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalMaterials = _materialsRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .Count();
                }
                else
                {
                    materials = _materialsRepository
                        .GetAll()
                        .OrderBy(m => m.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalMaterials = _materialsRepository.GetAll().Count();
                }

                IEnumerable<MaterialViewModel> materialsVM = Mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(materials);

                PaginationSet<MaterialViewModel> pagedSet = new PaginationSet<MaterialViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalMaterials,
                    TotalPages = (int)Math.Ceiling((decimal)totalMaterials / currentPageSize),
                    Items = materialsVM
                };

                response = request.CreateResponse<PaginationSet<MaterialViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, MaterialViewModel material)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Material newMaterial = new Material();
                    newMaterial.UpdateMaterial(material);

                    for (int i = 0; i < material.NumberOfTags; i++)
                    {
                        Tag tag = new Tag() {};
                        newMaterial.Tags.Add(tag);
                    }

                    _materialsRepository.Add(newMaterial);

                    _unitOfWork.Commit();

                    // Update view model
                    material = Mapper.Map<Material, MaterialViewModel>(newMaterial);
                    response = request.CreateResponse<MaterialViewModel>(HttpStatusCode.Created, material);
                }

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, MaterialViewModel material)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var materialDB = _materialsRepository.GetSingle(material.ID);
                    if (materialDB == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid material.");
                    else
                    {
                        materialDB.UpdateMaterial(material);
                        material.Image = materialDB.Image;
                        _materialsRepository.Edit(materialDB);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<MaterialViewModel>(HttpStatusCode.OK, material);
                    }
                }

                return response;
            });
        }

        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int materialId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var materialOld = _materialsRepository.GetSingle(materialId);
                if (materialOld == null)
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
                else
                {
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/materials");

                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                    // Read the MIME multipart asynchronously 
                    Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                    string _localFileName = multipartFormDataStreamProvider
                        .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                    // Create response
                    FileUploadResult fileUploadResult = new FileUploadResult
                    {
                        LocalFilePath = _localFileName,

                        FileName = Path.GetFileName(_localFileName),

                        FileLength = new FileInfo(_localFileName).Length
                    };

                    // update database
                    materialOld.Image = fileUploadResult.FileName;
                    _materialsRepository.Edit(materialOld);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
                }

                return response;
            });
        }
    }
}