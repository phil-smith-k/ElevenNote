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

        //GetById

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
