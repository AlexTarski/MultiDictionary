using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result != null) return Ok(_mapper.Map<WordViewModel>(result));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get word {ex}", ex);
                return BadRequest("Failed to get word");
            }
        }

        [HttpGet("glossary/{glossaryId:int}")]
        public async Task<IActionResult> GetByGlossary(int glossaryId)
        {
            try
            {
                var result = await _service.GetWordsByGlossaryAsync(glossaryId);
                if(!result.Any())
                    return NotFound($"No words found for Glossary ID {glossaryId}");
                return Ok(_mapper.Map<IEnumerable<WordViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get words {ex}", ex);
                return BadRequest("Failed to get words");
            }
        }

        [HttpGet("glossary/{glossaryId:int}/theme")]
        public async Task<IActionResult> GetByTheme(int glossaryId, [FromQuery] string theme)
        {
            try
            {
                var result = await _service.GetWordsByThemeAsync(glossaryId, theme);
                if (result == null || !result.Any())
                    return NotFound($"No words found for Glossary ID {glossaryId} and Theme '{theme}'");
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
