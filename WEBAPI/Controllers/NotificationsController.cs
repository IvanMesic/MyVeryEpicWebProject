using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.DALModels; // Import your Notification class and DbContext here

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly RwaMoviesContext _context;

        public NotificationsController(RwaMoviesContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public ActionResult<IEnumerable<Notification>> GetNotifications()
        {
            var notifications = _context.Notifications.ToList();
            return Ok(notifications);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public ActionResult<Notification> GetNotification(int id)
        {
            var notification = _context.Notifications.Find(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // POST: api/Notifications
        [HttpPost]
        public ActionResult<Notification> CreateNotification([FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            notification.CreatedAt = DateTime.Now;
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.Id }, notification);
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public ActionResult<Notification> UpdateNotification(int id, [FromBody] Notification updatedNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedNotification.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedNotification).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notifications.Any(n => n.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public ActionResult<Notification> DeleteNotification(int id)
        {
            var notification = _context.Notifications.Find(id);

            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            _context.SaveChanges();

            return Ok(notification);
        }
    }
}
