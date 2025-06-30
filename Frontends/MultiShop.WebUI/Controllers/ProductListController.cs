using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly ICommentService _commentService;
        public ProductListController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index(string CategoryID)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Ürün Listesi";
            ViewBag.CategoryID = CategoryID;
            return View();
        }

        public IActionResult ProductDetail(string ProductID)
        {
            ViewBag.FirstBreadcrump = "Ana Sayfa";
            ViewBag.SecondBreadcrump = "Ürün Detayları";
            ViewBag.ProductID = ProductID;
            return View();
        }

        [HttpGet]
        public PartialViewResult AddComment() 
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto) 
        {
            createCommentDto.ImageUrl = "https://cdn-icons-png.flaticon.com/512/9203/9203764.png";
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = true;
            await _commentService.CreateCommentAsync(createCommentDto);
            return RedirectToAction("Index", "Default");
        }
    }
}