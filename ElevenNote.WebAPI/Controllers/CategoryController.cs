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
        //Get
        public IHttpActionResult Get()
        {
            var service = CreateCategoryService();
            return Ok(service.GetAllCategories());
        }
        //GetById
        public IHttpActionResult Get(int id)
        {
            var service = CreateCategoryService();
            var category = service.GetCategoryById(id);
            return Ok(category);
        }
        //Post
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            var service = CreateCategoryService();

            if(!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }
        //Put
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
        //Delete

        //Create Category Service
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(this.User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}
