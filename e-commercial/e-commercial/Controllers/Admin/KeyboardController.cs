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
            return Ok(_keyboardService.GetKeyboardDetails(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] KeyboardCreateDTO keyboardDTO)
        {
            _keyboardService.CreateKeyboard(keyboardDTO);
            return Created("", null);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] KeyboardUpdateDTO keyboardDTO, Guid Id)
        {
            _keyboardService.UpdateKeyboard(keyboardDTO, Id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _keyboardService.DeleteKeyboard(id);
            return Ok();
        }

        [HttpGet("page")]
        public IActionResult GetPaginatedKeyboards([FromQuery] PaginationRequestDTO requestDTO, [FromQuery] string? name)
        {
            return Ok(_keyboardService.GetPagination(requestDTO, name));
        }
    }
}
