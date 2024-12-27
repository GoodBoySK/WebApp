using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace server.Utlis
{
    public class ErrorMesage
    {
        public string? Message { get; set; }
        public string? Details { get; set; }
        public IEnumerable<ValidationMessage> Errors { get; set; } = [];

    }

    public class ValidationMessage 
    {
        public required string Field { get; set; }
        public required string Message { get; set; }
    }
}