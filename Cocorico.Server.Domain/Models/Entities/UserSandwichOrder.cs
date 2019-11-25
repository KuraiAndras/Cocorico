using System;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class UserSandwichOrder : IDbEntity<Guid>
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;
        public CocoricoUser User { get; } = null!;

        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
