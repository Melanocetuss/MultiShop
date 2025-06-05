using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var values = await _mediator.Send(new GetOrderingQuery());
            return Ok(values);
        }

        [HttpGet("GetOrderingById")]
        public async Task<IActionResult> GetOrderingById(int orderingID)
        {
            var values = await _mediator.Send(new GetOrderingByIdQuery(orderingID));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command) 
        {
            await _mediator.Send(command);
            return Ok("Sipariş Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrdering(int orderingID)
        {
            await _mediator.Send(new RemoveOrderingCommand(orderingID));
            return Ok("Sipariş Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Sipariş Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}