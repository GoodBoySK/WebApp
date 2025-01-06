using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace server.Models;

public class User : IdentityUser 
{
    public bool Deleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; } = null;
}