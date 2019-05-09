using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.Interfaces.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace BLL.NothwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                ModelState.AddModelError("", "No data");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.CreateCategoryAsync(category);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(int id, [FromBody] Category category)
        {
            if (id <= 0)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(id, category);
                return Ok(category);
            }

            return NotFound();
        }

        //[HttpGet("{categoryPictureId}")]
        //public async Task<ActionResult> GetImageByCategoryId(int? categoryPictureId)
        //{
        //    if (categoryPictureId == null || categoryPictureId <= 0)
        //    {
        //        return NotFound();
        //    }

        //    var dbImage = await _categoryService.GetPictureByCategoryId(categoryPictureId);

        //    if (dbImage == null)
        //    {
        //        NotFound();
        //    }

        //    return Ok(dbImage);
        //}
    }
}