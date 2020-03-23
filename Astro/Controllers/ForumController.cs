using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult MainPage()
        {
            List<Topic> topics = _context.Topics.AsNoTracking().Include(t => t.User).OrderByDescending(t=>t.Id).ToList();

            return View(topics);
        }

        [Authorize]
        public IActionResult AddTopic()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddTopic(AddTopicViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.User.FirstOrDefault(t=>t.Id.Equals(_userManager.GetUserId(User)));

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

                _context.Add(topic);
                _context.Add(comment);
                _context.SaveChanges();

                return RedirectToAction("ShowTopic", new { id = topic.Id });
            }
            return View(model);
        }

        public IActionResult ShowTopic(int id)
        {
            Topic topic = _context.Topics.AsNoTracking().Include(t => t.Comments).ThenInclude(t=>t.User).Include(t => t.User).FirstOrDefault(t => t.Id == id);

            return View(topic);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(int topicId, string comment)
        {
            Topic topic = _context.Topics.FirstOrDefault(t => t.Id == topicId);

            User user = _context.User.FirstOrDefault(t => t.Id.Equals(_userManager.GetUserId(User)));

            Comment newComment = new Comment()
            {
                Content = comment,
                Date = DateTime.Now.ToString(),
                User = user,
                Topic = topic
            };

            _context.Add(newComment);
            _context.SaveChanges();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditComment(int id, int topicId, string comment)
        {
            Comment editComment = _context.Comments.FirstOrDefault(t => t.Id == id);

            editComment.Content = comment;
            editComment.Date = DateTime.Now.ToString();

            _context.SaveChanges();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteTopic(int id)
        {
            Topic topic = _context.Topics.Include(t=>t.Comments).FirstOrDefault(t => t.Id == id);

            foreach(Comment comment in topic.Comments)
            {
                _context.Remove(comment);
            }

            _context.Remove(topic);
            _context.SaveChanges();

            return RedirectToAction("MainPage");
        }

        [Authorize(Roles="Administrator")]
        public IActionResult DeleteComment(int id, int topicId)
        {
            Comment comment = _context.Comments.FirstOrDefault(t => t.Id == id);

            _context.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("ShowTopic", new { id = topicId });
        }

        [Authorize]
        public IActionResult ChangeRate(int id, bool up)
        {
            Topic topic = _context.Topics.FirstOrDefault(t => t.Id == id);

            if (up)
                topic.Rate++;
            else
                topic.Rate--;

            _context.SaveChanges();

            return RedirectToAction("ShowTopic", new { id = id });
        }
    }
}