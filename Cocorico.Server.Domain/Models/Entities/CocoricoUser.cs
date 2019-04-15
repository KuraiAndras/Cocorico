using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
