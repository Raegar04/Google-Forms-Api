using Application.CQRS.Commands.FormActions;
using Application.Responses;
using AutoMapper;
using Domain.Models;
using GoogleFormsApi.Requests.Form;
using GoogleFormsApi.Responses;

public class FormProfile : Profile
{
    /// <summary>
    /// Specified configuration for mapper
    /// </summary>
    public FormProfile()
    {
        CreateMap<Add.Command, Form>();
        CreateMap<Form, FormResponse>();
        CreateMap<Form, FormDetailsResponse>();
        CreateMap<UpdateFormRequest, Update.Command>();
        CreateMap<Update.Command, Form>();
        CreateMap<AddFormRequest, Add.Command>();
    }
}
