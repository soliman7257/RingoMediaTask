
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace RingoMediaTask.Services
{
    public class EmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly ApplicationDbContext _dbContext; 
        private readonly SmtpClient _smtpClient;

        public EmailSenderService(ILogger<EmailSenderService> logger, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
       

        public EmailSenderService()
        {
            _smtpClient = new SmtpClient("smtp.example.com") // Replace with your SMTP server address
            {
                Port = 587, // Use appropriate port for your SMTP server
                Credentials = new NetworkCredential("username", "password"), // Replace with your SMTP credentials
                EnableSsl = true, // Enable SSL if required
            };
        }


        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@example.com"), // Replace with your "from" email address
                Subject = subject,
                Body = message,
                IsBodyHtml = true, // Set to true if the message contains HTML
            };

            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }


}
