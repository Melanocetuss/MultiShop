using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop_Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

        public AddressesController(GetAddressQueryHandler getAddressQueryHandler,
                                   GetAddressByIdQueryHandler getAddressByIdQueryHandler,
                                   CreateAddressCommandHandler createAddressCommandHandler,
                                   UpdateAddressCommandHandler updateAddressCommandHandler,
                                   RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddressCommandHandler = updateAddressCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await _getAddressQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(int addressID)
        {
            var values = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(addressID));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {
            await _createAddressCommandHandler.Handle(command);
            return Ok("Adres Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAddress(int addressID)
        {
            await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(addressID));
            return Ok("Adres Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _updateAddressCommandHandler.Handle(command);
            return Ok("Adres Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }
    }
}