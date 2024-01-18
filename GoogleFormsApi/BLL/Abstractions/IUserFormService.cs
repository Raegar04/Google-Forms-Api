using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUserFormService : IGenericService<UserForm>
    {
        Task<IEnumerable<UserForm>> GetByForm(Guid formId);

        Task<IEnumerable<UserForm>> GetByUser(Guid userId);
    }
}
