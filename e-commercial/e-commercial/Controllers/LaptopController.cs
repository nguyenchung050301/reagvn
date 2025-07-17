using e_commercial.DTOs.Request.Laptop;
using e_commercial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly LaptopService _laptopService;
        public LaptopController(LaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailByID(Guid id)
        {            
            return Ok(_laptopService.GetLaptopDetails(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_laptopService.GetAllLaptopDetails());
        }

        [HttpPost]
        public IActionResult Create(LaptopCreateDTO laptopDTO)
        {
            try
            {
                _laptopService.CreateLaptop(laptopDTO);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }         
        }
        [HttpPut("{id}")] 
        public IActionResult Update(LaptopUpdateDTO laptopDTO, Guid id)
        {
            try
            {
                _laptopService.UpdateLaptop(laptopDTO, id);
                return Ok(laptopDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _laptopService.Re(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
