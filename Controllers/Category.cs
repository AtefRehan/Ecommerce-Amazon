using AutoMapper;
using ECommerce.DTOS.Category;
using ECommerce.DTOS.Supplier;
using ECommerce.Repositories.Catergory_Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Category:ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;

        public Category(ICategoryRepository _categoryRepo , IMapper _mapper)
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
    }
}
