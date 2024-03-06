using Domain.Models;
using GoogleFormsApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class FormDetailsResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Closed { get; set; }

        public UserResponse Holder { get; set; }

        public ICollection<UserFormResponse> AssignedUsers { get; set; }

        public ICollection<QuestionResponse> Questions { get; set; }
    }
}
