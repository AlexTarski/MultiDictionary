using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain.Entities;
using MultiDictionary.WebAPI.ViewModels;

namespace MultiDictionary.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _service;
        private readonly ILogger<WordsController> _logger;
        private readonly IMapper _mapper;

        public WordsController(IWordService service,
            ILogger<WordsController> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Word>> Get() => await _service.GetAllAsync();

        [HttpGet]
        public async Task<IActionResult> GetAllWords()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<WordViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get words {ex}", ex);
                return BadRequest("Failed to get words");
            }
        }

    }
}
