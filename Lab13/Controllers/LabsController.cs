using Microsoft.AspNetCore.Mvc;
using ClassLibLab5;

namespace Lab13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabsController : ControllerBase
    {

        [HttpPost("lab1result")]
        public IActionResult Lab1Result([FromBody] string userInput)
        {
            try
            {
                var result = Lab1Lib.RunLab1(userInput);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("lab2result")]
        public IActionResult Lab2Result([FromBody] string userInput)
        {
            try
            {
                var result = Lab2Lib.RunLab2(userInput);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("lab3result")]
        public IActionResult Lab3Result([FromBody] string userInput)
        {
            try
            {
                var result = Lab3Lib.RunLab3(userInput);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
