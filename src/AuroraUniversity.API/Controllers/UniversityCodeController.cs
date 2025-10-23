using AuroraUniversity.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AuroraUniversity.API.Controllers;

public class UniversityCodeController : BaseController
{
    [HttpGet("validate")]
    public IActionResult ValidateCode([FromQuery] string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest(new { error = "Code is required." });

        var result = UniversityCodeParserService.CheckValidCode(code);

        if (!result.IsValid)
            return BadRequest(new { error = result.ErrorMessage });

        return Ok(new
        {
            result.Type,
            result.Year,
            result.Term,
            result.Code,
            result.Serial,
            result.Checksum
        });
    }
}
