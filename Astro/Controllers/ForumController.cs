using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    public class ForumController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AstroDbContext _context;

        public ForumController(UserManager<User> userManager, AstroDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> MainPage()
        {
            List<Topic> topics = await _context.Topics.AsNoTracking().Include(t => t.User).OrderByDescending(t => t.Id)
                .ToListAsync();

            return View(topics);
        }

        [Authorize]
        public IActionResult AddTopic()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTopic(AddTopicViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(t => t.Id.Equals(_userManager.GetUserId(User)));

                Topic topic = new Topic()
                {
                    Title = model.Topic,
                    Rate = 0,
                    Date = DateTime.Now.ToString(),
                    User = user
                };

                Comment comment = new Comment()
                {
                    Content = model.Comment,
                    Date = DateTime.Now.ToString(),
                    User = user,
                    Topic = topic
                };

                user.TopicsCount++;
                user.CommentsCount++;

                _context.Update(user);
                await _context.AddAsync(topic);
                await _context.AddAsync(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowTopic", new { id = topic.Id });
            }
            return View(model);
        }

        public async Task<IActionResult> ShowTopic(int id)
        {
            Topic topic = await _context.Topics.AsNoTracking().Include(t => t.Comments).ThenInclude(t => t.User)
                .Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            return View(topic);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(int topicId, string comment)
        {
            Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == topicId);

            User user = await _context.User.FirstOrDefaultAsync(t => t.Id.Equals(_userManager.GetUserId(User)));

            Comment newComment = new Comment()
            {
                Content = comment,
                Date = DateTime.Now.ToString(),
                User = user,
                Topic = topic
            };

            user.CommentsCount++;

            _context.Update(user);
            await _context.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditComment(int id, int topicId, string comment)
        {
            Comment editComment = await _context.Comments.FirstOrDefaultAsync(t => t.Id == id);

            editComment.Content = comment;
            editComment.Date = DateTime.Now.ToString();

            await _context.SaveChangesAsync();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize(Roles = "Administrator")]
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

            return RedirectToAction("MainPage");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteComment(int id, int topicId)
        {
            Comment comment = await _context.Comments.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            User user = comment.User;
            user.CommentsCount--;

            _context.Update(user);
            _context.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize]
        public async Task<IActionResult> ChangeRate(int id, bool up)
        {
            Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

            if (up)
                topic.Rate++;
            else
                topic.Rate--;

            await _context.SaveChangesAsync();

            return RedirectToAction("ShowTopic", new { id = id });
        }
    }
}