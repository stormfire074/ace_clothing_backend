using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapp.Application;
using webapp.Infrastrcture;

namespace Infrastructure.Services
{
    public class CategoryService : Service<Category>
    {
        private readonly DatabaseContext _dbContext;
        public CategoryService(IRepository<Category> repository, DatabaseContext _dbContext) : base(repository)
        {
            this._dbContext = _dbContext;
        } 

    }
}
