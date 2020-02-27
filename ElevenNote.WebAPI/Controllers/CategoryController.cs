using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        public IHttpActionResult Get()
        {
            var service = CreateCategoryService();
            return Ok(service.GetAllCategories());
        }
        public IHttpActionResult Get(int id)
        {
            var service = CreateCategoryService();
            var category = service.GetCategoryById(id);
            return Ok(category);
        }
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            var service = CreateCategoryService();

            if(!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!this.ModelState.IsValid)
                return BadRequest();

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();
            else
                return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();
            if (!service.DeleteCategoryById(id))
                return InternalServerError();
            else
                return Ok();
        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(this.User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}
