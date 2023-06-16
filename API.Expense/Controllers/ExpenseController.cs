using Data.Extensions;
using Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.ValidationFilters;
using Data.Expense.DTOs;
using MODELS = Data.Expense.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Expense.Controllers
{
    [ServiceFilter(type: typeof(ValidationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironemnt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironemnt,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ExpenseController> logger)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironemnt = webHostEnvironemnt;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        // GET: api/expense
        [HttpGet]
        public async Task<ActionResult<IList<MODELS.Expense>>> Get()
        {
            var expenses = await _unitOfWork.ExpenseRepository.GetAll(c=> c.UserId == HttpContext.User.GetUserId(), includes: new List<string> { "Category", "Currency", "User" });
            return Ok(expenses);
        }

        // GET api/expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MODELS.Expense>> Get(int id)
        {
            var expense = await _unitOfWork.ExpenseRepository.Get(c => c.Id == id && c.UserId == HttpContext.User.GetUserId(), includes: new List<string> { "Category", "Currency", "User" });
            return Ok(expense);
        }

        // POST api/expense
        [HttpPost]
        public async Task<IActionResult> Post(ExpenseDto expenseDto)
        {
            if (TryStoreFile(expenseDto.Image, expenseDto.ImageName, out var internalImageName))
                expenseDto.ImageName = internalImageName;

            expenseDto.UserId = User.GetUserId();

            var expense = await _unitOfWork.ExpenseRepository.Add(expenseDto);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(expense);
        }

        // PUT api/expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MODELS.Expense expense)
        {
            if (id != expense.Id)
                return BadRequest();

            if (TryStoreFile(expense.Image, expense.ImageName, out var internalImageName))
                expense.ImageName = internalImageName;

            expense.UserId = User.GetUserId();

            await _unitOfWork.ExpenseRepository.Update(id, expense);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok(expense);
        }

        // DELETE api/expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.ExpenseRepository.Delete(id);
            await _unitOfWork.SaveChanges(HttpContext);

            return Ok();
        }

        private bool TryStoreFile(byte[] image, string imageName, out string internalName)
        {
            internalName = null;

            if (image == null || image.Length == 0)
                return false;

            var url = _httpContextAccessor.HttpContext.Request.Host.Value;
            var uploadsFolder = $"{_webHostEnvironemnt.WebRootPath}\\uploads";

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(imageName);
            imageName = $"{Guid.NewGuid().ToString().Replace("-", "")}{extension}";
            var path = $"{uploadsFolder}\\{imageName}";

            using var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);

            internalName = $"https://{url}/uploads/{imageName}";

            return internalName != null;
        }
    }
}
