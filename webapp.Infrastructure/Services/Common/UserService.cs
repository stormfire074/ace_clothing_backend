using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using webapp.Application;
using webapp.Infrastrcture;

namespace Infrastructure.Services
{
    public class UserService : Service<User>
    {
        private readonly DatabaseContext _dbContext;
        public UserService(IRepository<User> repository, DatabaseContext _dbContext) : base(repository)
        {
            this._dbContext = _dbContext;
        }

        public async Task<SaveResponse> AddUser(User_AddEdit request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return new SaveResponse(exception: new Exception("User Already Exists"));

            }
            var entity = new User(request);
            var roles = await _dbContext.Roles.Where(r => request.UserRoles.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                entity.UserRoles.Add(new UserRole { User = entity, Role = role });
            }
            return await base.AddAsync(entity);
        }

        public async Task<ListResponse<User_Listing>> ListUsers(ListRequest request)
        {
            var query = _dbContext.Set<User>().AsQueryable()
                .Skip(request.Skip).Take(request.Take);

            var totalCount = await query.CountAsync();
            var items = await query.Select(x=>new User_Listing()
            {
                Email=x.Email,
                FirstName=x.FirstName,
                LastName=x.LastName,
                DisplayName=x.DisplayName,
                Status=x.Status

            }).ToListAsync();
            return new ListResponse<User_Listing>(request: request)
            {
                Entities = items,
                TotalCount = totalCount
            };

        }
    }
}
