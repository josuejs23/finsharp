using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Helpers;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentInDb = await _context.Comments.FindAsync(id);
            if(commentInDb == null)
            {
                return null;
            }
            _context.Comments.Remove(commentInDb);
            await _context.SaveChangesAsync();
            return commentInDb;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
        {
            var comments = _context.Comments.Include(c => c.AppUser).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                comments.Where( c=> c.Stock.Symbol == query.Symbol);
            }

            if(query.IsDecsending)
            {
                comments.OrderByDescending(c => c.CreatedOn);
            }
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(c=>c.AppUser).FirstOrDefaultAsync( c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var commentInDb = await _context.Comments.FindAsync(id);
            if(commentInDb == null)
            {
                return null;
            }

            commentInDb.Title = comment.Title;
            commentInDb.Content = comment.Content;

            _context.SaveChanges();
            return commentInDb;
        }
    }
}