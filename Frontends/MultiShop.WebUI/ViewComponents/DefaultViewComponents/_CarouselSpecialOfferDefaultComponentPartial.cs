﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CarouselSpecialOfferDefaultComponentPartial : ViewComponent
    {
        private readonly ISpecialOfferService _specialOfferService;
        public _CarouselSpecialOfferDefaultComponentPartial(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }
    }
}