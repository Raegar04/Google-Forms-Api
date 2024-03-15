using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IQuestionService : IGenericService<Question>
    {
        Task<IEnumerable<Question>> GetByForm(Guid formId);
    }
}
