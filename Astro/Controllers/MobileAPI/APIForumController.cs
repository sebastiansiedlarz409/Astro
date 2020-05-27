using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers.MobileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class APIForumController : ControllerBase
    {
        private readonly AstroDbContext _context;
        private readonly UserManager<User> _userManager;

        public APIForumController(UserManager<User> userManager, AstroDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("Topic")]
        public async Task<ActionResult<IEnumerable<Topic>>> AllTopics()
        {
            return await _context.Topics.AsNoTracking().Include(t => t.User).ToListAsync();
        }

        [HttpGet("Topic/{id}")]
        public async Task<ActionResult<Topic>> Topic(int id)
        {
            Topic topic = await _context.Topics.AsNoTracking().OrderByDescending(t=>t.Id).Include(t => t.Comments).ThenInclude(t=>t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
            topic.User = null;

            topic.Comments.ForEach(t => t.User.Comments = null);
            topic.Comments.ForEach(t => t.User = new DAL.Models.User()
            {
                Id = t.User.Id.ToString(),
                UserName = t.User.UserName,
                Email = t.User.Email,
                Avatar = t.User.Avatar,
                TopicsCount = t.User.TopicsCount,
                CommentsCount = t.User.CommentsCount,
                LastLoginDate = t.User.LastLoginDate,
                RegisterDate = t.User.RegisterDate
            });
            return topic;
        }

        [HttpPost("Topic")]
        public async Task<IActionResult> AddTopic(AddTopicViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            User user = await _context.User.FirstOrDefaultAsync(t => t.Id.Equals(model.UserId));

            Topic topic = new Topic()
            {
                Title = model.Topic,
                Rate = 0,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                User = user
            };

            Comment comment = new Comment()
            {
                Content = model.Comment,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                User = user,
                Topic = topic
            };

            user.TopicsCount++;
            user.CommentsCount++;

            _context.Update(user);
            await _context.AddAsync(topic);
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Comment")]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            if (model == null)
                return BadRequest();

            Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == model.TopicId);

            User user = await _context.User.FirstOrDefaultAsync(t => t.Id.Equals(model.UserId));

            Comment newComment = new Comment()
            {
                Content = model.Comment,
                Date = DateTime.Now.ToString(),
                User = user,
                Topic = topic
            };

            user.CommentsCount++;

            _context.Update(user);
            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Comment")]
        public async Task<IActionResult> EditComment(AddCommentViewModel model)
        {
            if (model == null)
                return BadRequest();

            Comment editComment = await _context.Comments.FirstOrDefaultAsync(t => t.Id == model.Id);

            editComment.Content = model.Comment;
            editComment.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Topic/{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            Topic topic = await _context.Topics.Include(t => t.Comments).ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            User user = topic.User;

            foreach (Comment comment in topic.Comments)
            {
                User commentAuthor = comment.User;
                commentAuthor.CommentsCount--;

                _context.Update(commentAuthor);
                _context.Remove(comment);
            }

            user.TopicsCount--;

            _context.Update(user);

            _context.Remove(topic);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Comment comment = await _context.Comments.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            User user = comment.User;
            user.CommentsCount--;

            _context.Update(user);
            _context.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("Rate/{id}/{up}")]
        public async Task<IActionResult> ChangeRate(int id, int up)
        {
            Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

            if (up != 0)
                topic.Rate++;
            else
                topic.Rate--;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}