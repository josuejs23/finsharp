using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.models;
using api.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var commentsInDb = await _commentRepository.GetAllAsync();
            var commentsDto = commentsInDb.Select( c => c.ToCommentDto() );
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateComment([FromRoute] int id,  [FromBody] CreateCommentDto commentDto)
        {
            if(! await _stockRepository.StockExist(id))
            {
                return NotFound();
            }
            var commentModel = commentDto.ToCommentFromCreate(id);
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel}, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var commentDeleted = await _commentRepository.DeleteAsync(id);
            if(commentDeleted==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}