using System.Net;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        private ResponseVM _responseAPI;
        public ValuesController(IUserService userService)
        {
            _userService = userService;
            _responseAPI = new ResponseVM();
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseVM>> getAllData()
        {
            var data = await _userService.GetAll();
            if (data == null)
                return NotFound("User not avalible");
            _responseAPI.Data = data;
            _responseAPI.status = true;
            _responseAPI.statusCode = HttpStatusCode.OK;
            return _responseAPI;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseVM>> CreateUser([FromBody] RegisterUser model)
        {
            if (model == null)
                return BadRequest("Enter Valid Data");

            var data = await _userService.RegisterUser(model);

            _responseAPI.Data = data;
            _responseAPI.status = true;
            _responseAPI.statusCode = HttpStatusCode.OK;
            return _responseAPI;
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseVM>> UpdateUser(int id, [FromBody] RegisterUser model)
        {
            if (model == null)
                return BadRequest("Enter Valid Data");

            var data = await _userService.UpdateUser(id, model);

            _responseAPI.Data = data;
            _responseAPI.status = true;
            _responseAPI.statusCode = HttpStatusCode.OK;
            return _responseAPI;
        }

        [HttpDelete]
        [Route("Delte/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseVM>> DelteUser(int id)
        {
            if (id <= 0) return BadRequest("Put Valid ID");

            var data = await _userService.DelteUser(id);

            if (data == "User Not Found")
                return NotFound("User not found with this ID");

            _responseAPI.Data = data;
            _responseAPI.status = true;
            _responseAPI.statusCode = HttpStatusCode.OK;
            return _responseAPI;
        }
    }
}
