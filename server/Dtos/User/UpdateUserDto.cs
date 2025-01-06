using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.User
{
    public class UpdateUserDto
    {
        public required string NewName { get; set; }
    }
}