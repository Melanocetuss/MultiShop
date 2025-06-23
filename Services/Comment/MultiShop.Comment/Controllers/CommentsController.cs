using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _commentContext;
        public CommentsController(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList() 
        {
            var values = await _commentContext.UserComments.ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetCommentById")]
        public async Task<IActionResult> GetCommentById(int UserCommentID) 
        {
            var values = await _commentContext.UserComments.FindAsync(UserCommentID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment userComment) 
        {
            await _commentContext.UserComments.AddAsync(userComment);
            await _commentContext.SaveChangesAsync();
            return Ok("Yorum Ekleme İşlemi Başarıyla Gerçekleşti");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int UserCommentID) 
        {
            var values = await _commentContext.UserComments.FindAsync(UserCommentID);
            _commentContext.UserComments.Remove(values);
            await _commentContext.SaveChangesAsync();
            return Ok("Yorum Silme İşlemi Başarıyla Gerçekleşti");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UserComment userComment)
        {
            _commentContext.UserComments.Update(userComment);
            await _commentContext.SaveChangesAsync();
            return Ok("Yorum Günceleme İşlemi Başarıyla Gerçekleşti");

        }

        [HttpPut("CommentChangeStatusAsync")]
        public async Task<IActionResult> CommentChangeStatusAsync(int UserCommentID) 
        {
            var values = await _commentContext.UserComments.FindAsync(UserCommentID);
            values.Status = !values.Status;
            await _commentContext.SaveChangesAsync();
            return Ok("Yorum Durumu Günceleme İşlemi Başarıyla Gerçekleşti");
        }

        [HttpGet("GetCommentByProductID")]
        public async Task<IActionResult> GetCommentById(string ProductID)
        {
            var values = await _commentContext.UserComments.Where(x => x.ProductID == ProductID).ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetCommentCountByProductID")]
        public async Task<IActionResult> GetCommentCountByProductID(string ProductID)
        {
            var values = await _commentContext.UserComments
                .Where(x => x.ProductID == ProductID && x.Status == true)
                .CountAsync();
            return Ok(values);
        }
    }
}