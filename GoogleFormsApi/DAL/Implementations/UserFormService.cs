using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations
{
    public class UserFormService : GenericService<UserForm>, IUserFormService
    {
        public UserFormService(GoogleFormsDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
        }

        public async Task<IEnumerable<UserForm>> GetByForm(Guid formId)
        {
            return await _dbSet.Where(x => x.FormId == formId).ToListAsync();
        }

        public async Task<IEnumerable<UserForm>> GetByUser(Guid userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
