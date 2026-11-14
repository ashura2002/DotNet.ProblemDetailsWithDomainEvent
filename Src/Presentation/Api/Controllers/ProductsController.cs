using Api.Extensions;
using Api.Requests;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] CreateProductRequest request, CancellationToken ct)
        {
            var command = new CreateProductCommand(request.Name, request.Description);
            var result = await _mediator.Send(command, ct);
            return this.ToActionResult(result);
        }


        [HttpGet]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts(
            [FromQuery] PaginatedRequest request,
            CancellationToken ct = default)
        {
            var query = new GetAllProductsQuery(request.Page, request.PageSize);
            var result = await _mediator.Send(query, ct);
            return this.ToActionResult(result);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(
            [FromRoute] Guid id, CancellationToken ct)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, ct);
            return this.ToActionResult(result);
        }


        [HttpPut("{id:guid}/name")]
        public async Task<ActionResult> UpdateProductName(
            [FromRoute] Guid id,
            [FromBody] UpdateProductNameRequest request,
            CancellationToken ct)
        {
            var command = new UpdateProductNameCommand(id, request.NewProductName);
            var result = await _mediator.Send(command, ct);
            return this.ToActionResult(result);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken ct)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command, ct);
            return this.ToActionResult(result);
        }
    }
}
