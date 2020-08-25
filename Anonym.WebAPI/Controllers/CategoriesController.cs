using Anonym.Business.Abstract;
using Anonym.Entities.Concrete;
using Anonym.Entities.Dtos;
using AutoMapper;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Anonym.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IMapper _mapper;
        private ICategoryService _categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public IActionResult AddCategory([FromBody] CategoryForAddDto categoryForAddDto)
        {
            var category = _mapper.Map<Category>(categoryForAddDto);
            category.Active = false;
            category.CategoryId = Guid.NewGuid().ToString();

            IResult result = _categoryService.Add(category);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Message);
        }
    }
}
