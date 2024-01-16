using Application.CQRS.Commands.QuestionActions;
using AutoMapper;
using Domain.Models;
using GoogleFormsApi.Requests.Question;
using GoogleFormsApi.Responses;

namespace GoogleFormsApi.MapperProfiles
{
    public class QuestionProfile : Profile  
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionResponse>();
            CreateMap<AddQuestionRequest, Add.Command>();
            CreateMap<Add.Command, Question>();
            CreateMap<UpdateQuestionRequest, Update.Command>();
            CreateMap<Update.Command, Question>();
        }
    }
}
