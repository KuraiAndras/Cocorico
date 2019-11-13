using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }
    }
}
