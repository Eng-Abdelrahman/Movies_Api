using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context _dbContext;
        public CategoryController(Context dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        [Route("api/Category/GetCategories")]
        public async Task<IActionResult> GetCategoryies()
        {
            List<Category> categories = await _dbContext.Categories.ToListAsync();

            List<CategoryModel> categoryModels = categories.Select(q => new CategoryModel()
            {
                Id = q.Id,
                CategoryName = q.Name,
            }).ToList();

            return Ok(categoryModels);
        }


        [HttpGet]
        [Route("api/Category/GetCategory/{categoryId}")]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            Category category = await _dbContext.Categories.FirstOrDefaultAsync(q => q.Id == categoryId);
           
            CategoryModel categoryModel = new CategoryModel()
            {
                Id = category.Id,
                CategoryName = category.Name,
            };

            return Ok(categoryModel);
        }


        [HttpPost]
        [Route("api/Category/AddCategory")]
        public async Task<IActionResult> AddCategorye(AddCategory category)
        {
            Category newCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
            };

            await _dbContext.Categories.AddAsync(newCategory);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        [Route("api/Category/EditCategory")]
        public async Task<IActionResult> EditCategory(EditCategory category)
        {

            Category categoryToEdit = await _dbContext.Categories.FirstOrDefaultAsync(q => q.Id == category.Id);

            categoryToEdit.Name = category.Name;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        [Route("api/Category/DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            Category categoryToDelete = await _dbContext.Categories.FirstOrDefaultAsync(q => q.Id == categoryId);

            _dbContext.Categories.Remove(categoryToDelete);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
