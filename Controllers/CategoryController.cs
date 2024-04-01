using AutoMapper;
using ECommerce.DTOS.Category;
using ECommerce.DTOS.Supplier;
using ECommerce.Models;
using ECommerce.Repositories.Catergory_Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository _categoryRepo , IMapper _mapper)
        {
            categoryRepo = _categoryRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAll()
        {
            List<CategoryReadDTO> categories = new List<CategoryReadDTO>();
            foreach (var item in categoryRepo.GetAllCategories())
            {
                CategoryReadDTO c = mapper.Map<CategoryReadDTO>(item);
                categories.Add(c);
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryReadDTO> GetById(int id)
        {
            Category Target_category = categoryRepo.GetAllCategoriesById(id);
            if (Target_category == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CategoryReadDTO>(Target_category));
        }

        [HttpPost]
        public async Task<ActionResult> Add(CategoryCreateDTO c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdCategory = mapper.Map<Category>(c);
                    categoryRepo.Create(createdCategory);
                    categoryRepo.SaveChanges();
                    return CreatedAtAction("GetById", new { id = createdCategory.CategoryId }, c);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            categoryRepo.DeleteCategoryById(id);
            return Ok();
        }
    }
}
