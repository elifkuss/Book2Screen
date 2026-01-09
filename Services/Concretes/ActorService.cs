using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Services.Abstractions;

namespace Book2Screen.Services.Concretes
{
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<Movie> _movieRepository;

        public ActorService(IRepository<Actor> actorRepository, IRepository<Movie> movieRepository)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _actorRepository.GetAllAsync();
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await _actorRepository.GetAsync(a => a.ActorsId == id, a => a.Movie);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await _actorRepository.AddAsync(actor);
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            await _actorRepository.UpdateAsync(actor);
        }

        public async Task DeleteActorAsync(Actor actor)
        {
            await _actorRepository.DeleteAsync(actor);
        }

        public async Task<bool> ActorExistsAsync(int id)
        {
            return await _actorRepository.AnyAsync(a => a.ActorsId == id);
        }

        public async Task<IEnumerable<Actor>> SearchActorsAsync(string searchString)
        {
            var allActors = await _actorRepository.GetAllAsync();
            if (string.IsNullOrEmpty(searchString))
            {
                return allActors;
            }
            return allActors.Where(a => a.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }
    }
}

