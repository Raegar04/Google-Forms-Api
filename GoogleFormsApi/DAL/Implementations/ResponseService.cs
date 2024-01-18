using Application.Abstractions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations
{
    public class ResponseService : GenericService<Response>, IResponseService
    {
        public ResponseService(GoogleFormsDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<Response> responses)
        {
            await _dbSet.AddRangeAsync(responses);
            await _context.SaveChangesAsync();
        }
    }
}
