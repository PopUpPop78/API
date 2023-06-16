using Data.IRepositories;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.ValidationFilters;
using Data.Expense.DTOs;
using Data.Expense.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Expense.Controllers
{
    [ServiceFilter(type: typeof(ValidationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IExpenseUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IExpenseUnitOfWork unitOfWork, ILogger<CategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IList<Category>>> Get()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();

            return Ok(categories);
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            return Ok(category);
        }

        // POST api/category
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CategoryDto categoryDto)
        {
            var category = await _unitOfWork.CategoryRepository.Add(categoryDto);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(category);
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            await _unitOfWork.CategoryRepository.Update(id, category);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(category);
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        [Authorize(Roles = SD.RolesAdmin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.CategoryRepository.Delete(id);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok();
        }
    }
}
