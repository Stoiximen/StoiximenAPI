namespace Stoiximen.Domain.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
