namespace Cocorico.Server.Domain.Models
{
    public interface IDbEntity<T> : IDbEntity
    {
        T Id { get; set; }
    }

    public interface IDbEntity
    {
        bool IsDeleted { get; set; }
    }
}
