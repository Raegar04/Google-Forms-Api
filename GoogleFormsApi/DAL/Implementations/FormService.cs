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
    public class FormService : GenericService<Form>, IFormService
    {
        public FormService(GoogleFormsDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
        }

        public async Task<IEnumerable<Form>> GetByHolder(Guid holderId)
        {
            return await _dbSet.Where(x => x.HolderId == holderId).ToListAsync();
        }
    }
}
