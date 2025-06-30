using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            CommentViewBag();
            var values = await _commentService.GetAllCommentAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateComment(int id)
        {
            CommentViewBag();
            var values = await _commentService.GetByIdCommentAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            updateCommentDto.Status = false;
            updateCommentDto.CreatedDate = DateTime.Now;
            await _commentService.UpdateCommentAsync(updateCommentDto);
            return RedirectToAction("Index", "Comment", new { Area = "Admin" });
        }

        public async Task<IActionResult> CommentChangeStatus(int id) 
        {
            await _commentService.CommentChangeStatusAsync(id);
            return RedirectToAction("Index", "Comment", new { Area = "Admin" });
        }

        void CommentViewBag()
        {
            ViewBag.PageTitle = "Yorum İşlemeleri";
            ViewBag.index1 = "Ana Sayfa";
            ViewBag.index2 = "Yorumlar";
            ViewBag.index3 = "Yorum İşlemeleri";
        }
    }
}