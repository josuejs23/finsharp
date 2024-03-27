using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.models;
using api.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var commentsInDb = await _commentRepository.GetAllAsync();
            var commentsDto = commentsInDb.Select( c => c.ToCommentDto() );
            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int id,  [FromBody] CreateCommentDto commentDto)
        {
            if(! await _stockRepository.StockExist(id))
            {
                return NotFound();
            }

            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var commentModel = commentDto.ToCommentFromCreate(id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var commentDeleted = await _commentRepository.DeleteAsync(id);
            if(commentDeleted==null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateComment)
        {
            var commentUpdated = await _commentRepository.UpdateAsync(id, updateComment.ToCommentFromUpdate());
            if(commentUpdated==null)
            {
                return NotFound();
            }
            return Ok(commentUpdated.ToCommentDto());
        }
    }
}