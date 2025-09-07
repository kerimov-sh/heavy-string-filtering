using HeavyStringFiltering.Business.DataTransferObjects;
using HeavyStringFiltering.Business.Services;
using HeavyStringFiltering.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeavyStringFiltering.WebApi.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IStringChunkService _stringChunkService;

    public UploadController(IStringChunkService stringChunkService)
    {
        _stringChunkService = stringChunkService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponseModel>> UploadAsync(StringChunkDto stringChunk)
    {
        var isAdded = await _stringChunkService.AddChunkAsync(stringChunk);
        var response = new ApiResponseModel();

        if (isAdded)
        {
            response.Status = "Accepted";

            return Ok(response);
        }

        response.Status = "Rejected";

        return BadRequest(response);
    }
}