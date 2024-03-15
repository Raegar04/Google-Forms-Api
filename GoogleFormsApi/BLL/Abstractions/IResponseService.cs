using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IResponseService : IGenericService<Response>
    {
        Task AddRangeAsync(IEnumerable<Response> responses);
    }
}
