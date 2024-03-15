using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IFormService : IGenericService<Form>
    {
        Task<IEnumerable<Form>> GetByHolder(Guid holderId);
    }
}
