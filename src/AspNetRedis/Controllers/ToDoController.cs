using AspNetRedis.Infra.Caching;
using AspNetRedis.Infra.Persistence;
using AspNetRedis.Models;
using AspNetRedis.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetRedis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly ICacheService _cache;
        private readonly ToDoDbContext _context;

        public ToDoController(ILogger<ToDoController> logger, ICacheService cache, ToDoDbContext context)
        {
            _logger = logger;
            _cache = cache;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todoCache = await _cache.GetAsync(id.ToString());
            ToDo? todo;

            if (!string.IsNullOrWhiteSpace(todoCache))
            {
                todo = JsonConvert.DeserializeObject<ToDo>(todoCache);

                Console.WriteLine("Loadded from cache.");
                return Ok(todo);
            }

            todo = await _context.ToDos.SingleOrDefaultAsync(t => t.Id == id);

            if (todo == null)
                return NotFound();

            await _cache.SetAsync(id.ToString(), JsonConvert.SerializeObject(todo));

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoCreate request)
        {
            var todo = new ToDo(request.Title, request.Description);

            await _context.ToDos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, request);
        }
    }
}