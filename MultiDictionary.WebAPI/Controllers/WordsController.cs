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
                if (!result.Any())
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

        [HttpPost]
        public async Task<IActionResult> AddWordAsync([FromBody] WordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Assign defaults if null
                    model.Theme ??= "No theme";
                    model.Definition ??= "No definition";
                    model.AdditionalInfo ??= "No additional information about word";

                    var newWord = _mapper.Map<WordViewModel, Word>(model);
                    await _service.AddEntityAsync(newWord);

                    if (await _service.SaveAllAsync())
                    {
                        return Created($"/api/{newWord.Id}", _mapper.Map<Word, WordViewModel>(newWord)); //return HTTP 201 status (Created)
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save a new word: {ex}", ex);
                return StatusCode(500, "Unexpected error occurred.");
            }

            return StatusCode(500, "Unexpected error occurred.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWordAsync(int id)
        {
            try
            {
                var wordToDelete = await _service.GetByIdAsync(id);
                if(wordToDelete != null)
                {
                    _service.DeleteEntity(wordToDelete);
                    return await _service.SaveAllAsync() ? NoContent() : StatusCode(500, "Failed to delete a word");
                }
                else
                {
                    return NotFound($"Word with ID {id} not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to delete a word with ID {id}", id);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }
    }
}
