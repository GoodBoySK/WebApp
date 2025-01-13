using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.UserDro
{
    public class UpdateUserDto
    {
        public required string NewName { get; set; }
    }
}