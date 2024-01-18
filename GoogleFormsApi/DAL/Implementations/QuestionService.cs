using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations
{
    public class QuestionService : GenericService<Question>, IQuestionService
    {
        public QuestionService(GoogleFormsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetByForm(Guid formId)
        {
            return await _dbSet.Where(x => x.FormId == formId).ToListAsync();
        }
    }
}
