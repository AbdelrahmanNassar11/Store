using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]// /api/Buggy/notfound
        public IActionResult GetNotFoundErrorRequest()
        {
            return NotFound();// 404
        }

        [HttpGet("servererror")]// /api/Buggy/servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")]// /api/Buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(); // 400
        }

        [HttpGet("badrequest/{id}")]// /api/Buggy/badrequest/nassar
        public IActionResult GetBadRequest(int id)//Validation Error
        {
            return BadRequest(); // 400
        }

        [HttpGet("unauthorized")]// /api/Buggy/unauthorized
        public IActionResult GetUnauthorizedRequest()//Validation Error
        {
            return Unauthorized(); // 400
        }
    }
}

