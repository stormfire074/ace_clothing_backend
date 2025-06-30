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
            var items = await query.Select(x => new User_Listing()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DisplayName = x.DisplayName,
                Status = x.Status

            }).ToListAsync();
            return new ListResponse<User_Listing>(request: request)
            {
                Entities = items,
                TotalCount = totalCount
            };

        }
        public async Task<RetrieveResponse<User_AddEdit>> RetrieveUser(Guid Id)
        {
            var response = await _dbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Where(x => x.Id == Id).Select(usr => new User_AddEdit()
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Email = usr.Email,
                Status = usr.Status,
                UserRoles = (ICollection<string>)usr.UserRoles.Select(x => x.Role.Name),

            }).FirstOrDefaultAsync();
            return new RetrieveResponse<User_AddEdit>(response);
        }
        public async Task<SaveResponse> UpdateUser(User_AddEdit request)
        {
            var entity = await _dbContext.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity != null)
            {
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Status = request.Status;
                entity.Email = request.Email;
                entity.DisplayName = request.GetDisplayName();
                var roles = await _dbContext.Roles.Where(r => request.UserRoles.Contains(r.Name)).ToListAsync();
                entity.UserRoles.Clear();
                foreach (var role in roles)
                {
                    entity.UserRoles.Add(new UserRole { User = entity, Role = role });
                }
                return await base.UpdateAsync(entity);
            }
            else
            {
                return new SaveResponse(exception: new Exception("Record not found."));
            }

        }
    }
}
