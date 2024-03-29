using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.SupplierRepository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ECommerce.DTOS.Product;
using ECommerce.Models;
using ECommerce.DTOS.Supplier;
using AutoMapper;
namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        private readonly ISupplierRepository supplierRepo;
        private readonly IMapper mapper;

        public SupplierController(ISupplierRepository _supplierRepo , IProductRepository _productRepo , IMapper _mapper)
        {
            productRepo = _productRepo;
            supplierRepo = _supplierRepo;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAll()
        {
            List<SupplierDTO> suppliers = new List<SupplierDTO>();
            foreach (var item in supplierRepo.GetAllSuppliers())
            {
                SupplierDTO s = mapper.Map<SupplierDTO>(item);
                suppliers.Add(s);
            }
            return Ok(suppliers);
        }


        [HttpGet("{id}")]
        public ActionResult<SupplierDTO> GetById(int id)
        {
            Supplier supplier = supplierRepo.GetAllSuppliersById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SupplierDTO>(supplier));
        }


        [HttpPost]
        public async Task<ActionResult> Add(SupplierCreateDTO s)
        {
            if (s == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return NotFound();
            else
            {
                var supplier = mapper.Map<Supplier>(s);
                supplierRepo.Create(supplier);
                supplierRepo.SaveChanges();
                return CreatedAtAction("GetById", new { id = supplier.SupplierId }, s);

            }
        }

        //////////////////// add product to update
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, SupplierUpdateDTO supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.supplierId != id)
                {
                    return BadRequest();
                }
                    var supplierToEdit = supplierRepo.GetById(id);
                    if (supplierToEdit is null)
                    {
                        return NotFound();
                    }
                    mapper.Map(supplier, supplierToEdit);
                    supplierRepo.Update(supplierToEdit);
                    supplierRepo.SaveChanges();
                    return Ok(mapper.Map<SupplierDTO>(supplierToEdit));
                
            }
            return BadRequest(ModelState);
        }
        //test product
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            supplierRepo.DeleteSupplierById(id);

            return Ok();
        }

    }
}
