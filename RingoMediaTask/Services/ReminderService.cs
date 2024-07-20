
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using System.Collections.Generic;

namespace RingoMediaTask.Services
{
    public class ReminderService
    {
        private readonly ILogger<ReminderService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly EmailSenderService _emailSender;

        public ReminderService(ILogger<ReminderService> logger, ApplicationDbContext dbContext ,EmailSenderService emailSender)
        {
            _dbContext = dbContext;
            _logger = logger;
            _emailSender = emailSender;
        }


        public async Task CreateReminderAsync(Reminder reminder)
        {
            _dbContext.Reminders.Add(reminder);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CheckRemindersAsync()
        {
            var now = DateTime.UtcNow;
            var reminders = await _dbContext.Reminders
                .Where(r => r.DateTime <= now && !r.IsSent)
                .ToListAsync();

            foreach (var reminder in reminders)
            {
               
                await _emailSender.SendEmailAsync("recipient@example.com", reminder.Title, "Reminder Notification");
                reminder.IsSent = true;
            }

            await _dbContext.SaveChangesAsync();
        }



    }
}
