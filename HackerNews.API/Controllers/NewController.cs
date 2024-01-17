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
    public class NewController : ControllerBase
    {

        private readonly INewService _service;

        public NewController(INewService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("search")]
        [SwaggerResponse(StatusCodes.Status200OK, ApiLiterals.SearchSummary, typeof(NewItemDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, ApiLiterals.Description401Unauthorized, type: typeof(Error401))]
        [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, ApiLiterals.Description403Forbidden, typeof(Error403))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, ApiLiterals.Description404NotFound, typeof(Error404))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, ApiLiterals.Description500InternalServerError, typeof(Error500))]
        public async Task<ActionResult<PageResult<NewItemDto>>> Search(string title, int pageSize, int pageIndex)
        {
            var result = await _service.SearchAsync(title, pageSize, pageIndex);
            return Ok(new PageResult<NewItemDto> ()
            {
                Page = result.data,
                PageSize = pageSize,
                PageIndex =  pageIndex,
                ResultsCount = result.count
            }) ;
        }
    }
}