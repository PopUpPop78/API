using Data.Expense.ViewModels.Create;
using Data.Expense.ViewModels.Read;
using Data.IRepositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Expense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(IUnitOfWork unitOfWork, ILogger<CurrencyController> logger) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        // GET: api/<CurrencyController>
        [HttpGet]
        public async Task<ActionResult<IList<ReadCurrency>>> Get()
        {
            var currencies = await _unitOfWork.CurrencyRepository.GetAll<ReadCurrency>();

            return Ok(currencies);
        }

        // GET api/<CurrencyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCurrency>> Get(int id)
        {
            return await _unitOfWork.CurrencyRepository.Get<ReadCurrency>(c => c.Id == id);
        }

        // POST api/<CurrencyController>
        [HttpPost]
        public async Task<ActionResult> Post(CreateCurrency currency)
        {
            await _unitOfWork.CurrencyRepository.Add(currency);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(currency);
        }

        // PUT api/<CurrencyController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateCurrency currency)
        {
            if (id != currency.Id)
                return BadRequest();

            await _unitOfWork.CurrencyRepository.Update(id, currency);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(currency);
        }

        // DELETE api/<CurrencyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.CurrencyRepository.Delete(id);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok();
        }
    }
}
