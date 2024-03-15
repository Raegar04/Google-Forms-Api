using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations.CachingServices
{
    public static class CachingConstants
    {
        public static Func<Guid, string> FormsByHolderId = id => $"forms-holder-{id}";

        public static Func<Guid, string> FormById = id => $"form-{id}";

        public static Func<Guid, string> QuestionsByFormId = id => $"questions-form-{id}";

        public static Func<Guid, string> QuestionById = id => $"question-{id}";

        public static Func<Guid, string> UserFormsByFormId = id => $"userForms-form-{id}";

        public static Func<Guid, string> UserFormsByUserId = id => $"userForms-user-{id}";

        public static Func<Guid, string> UserFormById = id => $"userForm-{id}";
    }
}
