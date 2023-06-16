using Data;
using Data.Expense.DTOs;
using Data.Expense.Models;
using Data.IRepositories;
using Data.ValidationFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Expense.Controllers
{
    [ServiceFilter(type: typeof(ValidationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IExpenseUnitOfWork _unitOfWork;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(IExpenseUnitOfWork unitOfWork, ILogger<CurrencyController> logger) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        // GET: api/currency
        [HttpGet]
        public async Task<ActionResult<IList<Currency>>> Get()
        {
            var currencies = await _unitOfWork.CurrencyRepository.GetAll();

            return Ok(currencies);
        }

        // GET api/currency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> Get(int id)
        {
            var currency = await _unitOfWork.CurrencyRepository.Get(c => c.Id == id);
            return Ok(currency);
        }

        // POST api/currency
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CurrencyDto currencyDto)
        {
            var currency = await _unitOfWork.CurrencyRepository.Add(currencyDto);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(currency);
        }

        // PUT api/currency/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Currency currency)
        {
            if (id != currency.Id)
                return BadRequest();

            await _unitOfWork.CurrencyRepository.Update(id, currency);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(currency);
        }

        // DELETE api/currency/5
        [HttpDelete("{id}")]
        [Authorize(Roles = SD.RolesAdmin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.CurrencyRepository.Delete(id);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok();
        }
    }
}
