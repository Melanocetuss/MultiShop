using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ReviewProductDetailComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;
        public _ReviewProductDetailComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string ProductID)
        {
            var commentCount = await _commentService.GetCommentCountByProductID(ProductID);
            var values = await _commentService.GetCommentByProductID(ProductID);
            ViewBag.CommentCount = commentCount;
            return View(values);           
        }
    }
}