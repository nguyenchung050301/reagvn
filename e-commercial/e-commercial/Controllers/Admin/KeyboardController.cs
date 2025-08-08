using e_commercial.Constants;
using e_commercial.DTOs.Request.Keyboard;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.Exceptions;
using e_commercial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers.Admin
{
    [Authorize(Roles = RoleEnum.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class KeyboardController : Controller
    {
        private readonly KeyboardServicce _keyboardService;
        public KeyboardController(KeyboardServicce keyboardService)
        {
            _keyboardService = keyboardService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_keyboardService.GetKeyboardAllDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailByID(Guid id)
        {
            try
            {
                return Ok(_keyboardService.GetKeyboardDetails(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] KeyboardCreateDTO keyboardDTO)
        {
            try
            {
                _keyboardService.CreateKeyboard(keyboardDTO);
                return Created("", null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] KeyboardUpdateDTO keyboardDTO, Guid Id)
        {
            try
            {
                _keyboardService.UpdateKeyboard(keyboardDTO, Id);
                return Ok();
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _keyboardService.DeleteKeyboard(id);
                return Ok();
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("page")]
        public IActionResult GetPaginatedKeyboards([FromQuery] PaginationRequestDTO requestDTO, [FromQuery] string? name)
        {
            try
            {
                var paginatedKeyboards = _keyboardService.GetPagination(requestDTO, name);
                return Ok(paginatedKeyboards);
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
