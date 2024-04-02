using AutoMapper;
using ECommerce.DTOS.SendEmail;
using ECommerce.DTOS.SubCategory;
using ECommerce.Models;
using ECommerce.Repositories.Catergory_Repository;
using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.SubCategory_Repository;
using ECommerce.Services;
using ECommerce.Services.MailV;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailService _emailService;


        public SubCategoryController(IMapper mapper,ICategoryRepository categoryRepository,ISubCategoryRepository subCategoryRepository,IProductRepository productRepository ,IEmailService emailService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
            _emailService = emailService;
        }


        [HttpGet]
        public ActionResult<List<SubCategoryReadDto>> GetAll()
        {
            var subCategories= _subCategoryRepository.GetAll();
            return _mapper.Map<List<SubCategoryReadDto>>(subCategories);
        }

        [HttpGet("{id}")]
        public ActionResult<SubCategoryReadDto> GetById(int id)
        {
            var subCategory = _subCategoryRepository.GetById(id);
            if (subCategory == null)
            {
                return BadRequest();
            }

            return _mapper.Map<SubCategoryReadDto>(subCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            _subCategoryRepository.Delete(id);
            _subCategoryRepository.SaveChanges();
            return NoContent();
        }
        
        
        [HttpPost]
        public IActionResult Create(SubCategoryWriteDto subCategoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
                    // c.Id = Guid.NewGuid();
                    var category = _categoryRepository.GetById(subCategoryDto.CategoryId);
                    subCategory.Category = category;
                    _subCategoryRepository.Create(subCategory);
                    _subCategoryRepository.SaveChanges();
                    return Ok(_mapper.Map<SubCategoryReadDto>(subCategory));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        
        
        
        // [HttpPost(Name = "SendEmail")]
        // public void SendEmail(SendEmailDto emailDto)
        // {
        //     _emailService.SendEmail(emailDto);
        // }
        //
    }
}
