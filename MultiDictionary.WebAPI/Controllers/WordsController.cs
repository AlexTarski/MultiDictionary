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
                _logger.LogError(ex, "Failed to get all words");
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(_mapper.Map<WordViewModel>(result));
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to get a word by ID {Id}", id);
                return NotFound($"Word with ID {id} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get a word by ID {Id}", id);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet("glossary/{glossaryId:int}")]
        public async Task<IActionResult> GetByGlossary(int glossaryId)
        {
            try
            {
                var result = await _service.GetWordsByGlossaryAsync(glossaryId);
                if (!result.Any())
                    return NotFound($"The Glossary with ID {glossaryId} is empty");
                return Ok(_mapper.Map<IEnumerable<WordViewModel>>(result));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to get words from Glossary with ID {Id}", glossaryId);
                return BadRequest($"Glossary with ID {glossaryId} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get words from Glossary with ID {Id}", glossaryId);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet("glossary/{glossaryId:int}/theme")]
        public async Task<IActionResult> GetByTheme(int glossaryId, [FromQuery] string theme)
        {
            try
            {
                var result = await _service.GetWordsByThemeAsync(glossaryId, theme);
                if (result == null || !result.Any())
                {
                    return NotFound($"No words with Theme '{theme}' were found in Glossary with ID {glossaryId}");
                }
                return Ok(_mapper.Map<IEnumerable<WordViewModel>>(result));
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to get words from Glossary with ID {Id}", glossaryId);
                return BadRequest($"Glossary with ID {glossaryId} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get words by theme {Theme} in Glossary with ID {Id}", theme, glossaryId);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWordAsync([FromBody] WordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newWord = _mapper.Map<WordViewModel, Word>(model);
                var success = await _service.AddEntityAsync(newWord);

                return success ? Created($"/api/{newWord.Id}", _mapper.Map<Word, WordViewModel>(newWord)) //return HTTP 201 status (Created)
                                          : StatusCode(500, "Failed to save a new word");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to save a new word with ID {Id}", model.Id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Failed to save a new word: {Message}", ex.Message);
                return BadRequest($"Word with ID {model.Id} already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save a new word with ID {Id}", model.Id);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWordAsync(int id)
        {
            try
            {
                var success = await _service.DeleteEntityAsync(id);
                return success ? NoContent() : StatusCode(500, "Failed to delete a word");
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to delete a word with ID {Id}", id);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to delete a word with ID {Id}", id);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }
    }
}