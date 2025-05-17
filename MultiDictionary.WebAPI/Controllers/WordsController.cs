using Microsoft.AspNetCore.Mvc;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain.Entities;

namespace MultiDictionary.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _service;
        private readonly ILogger<WordsController> _logger;

        public WordsController(IWordService service, ILogger<WordsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Word>> Get() => await _service.GetAllAsync();
    }
}
