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
        private readonly IFMPService _fmpService;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager, IFMPService fmpService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fmpService = fmpService;
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

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol,  [FromBody] CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("Stock does not exist!");
                }
                else
                {
                    await _stockRepository.CreateStockAsync(stock);
                }
            }

            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
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