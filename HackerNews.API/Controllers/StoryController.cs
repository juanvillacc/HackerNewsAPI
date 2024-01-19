using HackerNews.API.Errors;
using HackerNews.Domain.Common;
using HackerNews.Domain.Contracts;
using HackerNews.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HackerNews.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {

        private readonly IStoryService _service;

        public StoryController(IStoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("search")]
        [SwaggerResponse(StatusCodes.Status200OK, ApiLiterals.SearchSummary, typeof(StoryResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, ApiLiterals.Description401Unauthorized, type: typeof(Error401))]
        [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, ApiLiterals.Description403Forbidden, typeof(Error403))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, ApiLiterals.Description404NotFound, typeof(Error404))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, ApiLiterals.Description500InternalServerError, typeof(Error500))]
        public async Task<ActionResult<PageResult<StoryResponseDto>>> Search(string? title, int pageSize, int pageIndex)
        {
            var result = await _service.SearchAsync(!string.IsNullOrEmpty(title) ? title : String.Empty , pageSize, pageIndex);
            return Ok(new PageResult<StoryResponseDto> ()
            {
                Page = result.data,
                PageSize = pageSize,
                PageIndex =  pageIndex,
                ResultsCount = result.count
            }) ;
        }

        [HttpPost]
        public async Task<ActionResult> SynchronizeAsync()
        {
            await _service.SynchronizeAsync();
            return Ok();
        }
    }
}