using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Book2Screen.UnitOfWorks;
using static Book2Screen.UnitOfWorks.UnitOfWorks;
using Book2Screen.Repository.Concretes;
using Book2Screen.Services;
using Book2Screen.Services.Abstractions;
using Book2Screen.Services.Concretes;

namespace Book2Screen.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection LoadServiceExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<MovieDbContext>(opt => opt.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}



