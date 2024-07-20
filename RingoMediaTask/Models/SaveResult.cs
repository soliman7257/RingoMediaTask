﻿using Microsoft.AspNetCore.Mvc;

namespace RingoMediaTask.Models
{
    public class SaveResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public SaveResult(bool isSuccess, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
