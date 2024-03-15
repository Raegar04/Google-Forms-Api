using AutoMapper;
using Domain.Models;
using GoogleFormsApi.Requests.Answer;

namespace GoogleFormsApi.MapperProfiles
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<AddAnswerRequest, Response>();
        }
    }
}
