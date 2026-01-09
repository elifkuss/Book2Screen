using Book2Screen.Models;

namespace Book2Screen.Services.Abstractions
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);
        Task AddActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(Actor actor);
        Task<bool> ActorExistsAsync(int id);
        Task<IEnumerable<Actor>> SearchActorsAsync(string searchString);
    }
}

