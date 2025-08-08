using e_commercial.Constants;
using e_commercial.DTOs.Request.Order;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace e_commercial.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly JWTService _jwtService;
        private readonly UserService _userService;
        public OrderController(OrderService orderService, JWTService jwtService, UserService userService)
        {
            _orderService = orderService;
            _jwtService = jwtService;
            _userService = userService;
        }
        [HttpGet("admin/{id}")]
        public IActionResult GetOrderById(Guid id)
        {
            try
            {
              //  var order = _orderService.GetOrderDetails(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/admin")]
        public IActionResult GetAllOrders()
        {
          //  var orders = _orderService.GetAllOrderDetails();
            return Ok();
        }

        [HttpPost("/admin/order/pagination")] //dung post de dc phep su dung frombody
        public IActionResult GetOrdersPagination([FromBody] OrderPaginationRequestDTO paginationDTO)
        {
            try
            {
                var result = _orderService.GetPagination(paginationDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("admin/order/approve/{id}")]
        public IActionResult ApproveOrder(Guid id)
        {
            try
            {
                _orderService.AprroveOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RoleEnum.Admin)]
        [HttpPut("admin/order/cancel/{id}")]
        public IActionResult CanncelOrderByAdmin(Guid id)
        {
            try
            {
                _orderService.CancelOrderByAdmin(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RoleEnum.User)]
        [HttpPut("user/order/cancel/{id}")]
        public IActionResult CanncelOrderByUser(Guid id)
        {
            try
            {
                _orderService.CancelOrderByUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RoleEnum.User)]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateDTO orderDTO)
        {
            try
            {

                string userID = _jwtService.ExtractID(User);
                var existingUser = _userService.LoadByUserId(userID);
                _orderService.CreateOrder(orderDTO, existingUser);
                return Created("", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }                                                                                   
/*
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(Guid id, [FromBody] OrderCreateDTO orderDTO)
        {
            try
            {
                _orderService.UpdateOrder(id, orderDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("page")]
        public IActionResult Pagination([FromQuery] PaginationRequestDTO paginationDTO)
        {
            try
            {
                var result = _orderService.GetPagination(paginationDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
*/
    }
}