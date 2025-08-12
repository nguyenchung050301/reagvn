using e_commercial.Constants;
using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.Exceptions;
using e_commercial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers.Admin
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

        [Authorize(Roles = RoleEnum.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetDetailByID(Guid id)
        {            

            return Ok(_laptopService.GetLaptopDetails(id));
        }

        [Authorize (Roles = RoleEnum.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_laptopService.GetAllLaptopDetails());
    
        }

       [Authorize(Roles = RoleEnum.Admin)]
        [HttpPost]
        public IActionResult Create(LaptopCreateDTO laptopDTO)
        {

            _laptopService.CreateLaptop(laptopDTO);
            return Created();
        }

        [Authorize(Roles = RoleEnum.Admin)]
        [HttpPut("{id}")] 
        public IActionResult Update(LaptopUpdateDTO laptopDTO, Guid id)
        {
            _laptopService.UpdateLaptop(laptopDTO, id);
            return Ok(laptopDTO);
        }

        [Authorize(Roles = RoleEnum.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _laptopService.DeleteLaptop(id);
            return NoContent();
        }

        [Authorize(Roles = RoleEnum.Admin)]
        [HttpGet("page")]
        public IActionResult Pagination([FromQuery]PaginationRequestDTO paginationDTO, [FromQuery]string? name)
        {
            return Ok(_laptopService.GetPagination(paginationDTO, name));
        }
 
        private string GetHeaderAuthor()
        {
            if (Request.Headers.TryGetValue("Authorization", out var header))
            {
                var token = header.ToString();
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) //ko phan biet chu hoa chu thuong
                {
                    return token.Substring("Bearer ".Length).Trim();//lay ky tu tu` Bearer.Length tro di
                }
            }
            Console.WriteLine("Raw Authorization header: " + HttpContext.Request.Headers["Authorization"]);
            Console.WriteLine(HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            throw new BadValidationException("Authorization header sai", "Authorization");
           
         //   return HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //Lay Header Authorization tu Http Request, vd ket qua: Bearer ......, thay the Bearer = "" 
        }
    }
}
