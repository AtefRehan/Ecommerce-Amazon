using AutoMapper;
using ECommerce.DTOS.Supplier;
using ECommerce.Repositories.Order_Repository;
using ECommerce.Repositories.Payment_Repository;
using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.SupplierRepository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.DTOS;
using ECommerce.DTOS.Payment;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepo;
        private readonly IOrderRepository orderRepo;
        private readonly IMapper mapper;

        public PaymentController(IPaymentRepository _paymentRepository, IOrderRepository _orderRepository, IMapper _mapper)
        {
            paymentRepo = _paymentRepository;
            orderRepo = _orderRepository;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAll()
        {
            List<PaymentDTO> payments = new List<PaymentDTO>();
            foreach (var item in paymentRepo.GetAllPayments())
            {
                PaymentDTO s = mapper.Map<PaymentDTO>(item);
                payments.Add(s);
            }
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentDTO> GetById(int id)
        {
            Payment payment = paymentRepo.GetAllPaymentsById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PaymentDTO>(payment));
        }

        [HttpPost]
        public async Task<ActionResult> Add(PaymentCreateDTO p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = mapper.Map<Payment>(p);
                    paymentRepo.Create(payment);
                    paymentRepo.SaveChanges();
                    return CreatedAtAction("GetById", new { id = payment.PaymentId }, new { PaymentId = payment.PaymentId, Payment = p });

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}