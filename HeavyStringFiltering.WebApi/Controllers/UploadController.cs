using HeavyStringFiltering.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeavyStringFiltering.WebApi.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    public UploadController()
    {
        
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> UploadAsync(StringChunk chunk)
    {
        var result = new ApiResponse
        {
            Status = "Accepted"
        };

        return Ok(result);
    }
}