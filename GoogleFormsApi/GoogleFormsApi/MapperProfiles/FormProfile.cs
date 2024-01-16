using Application.CQRS.Commands.FormActions;
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
        CreateMap<UpdateFormRequest, Update.Command>();
        CreateMap<AddFormRequest, Add.Command>();
    }
}
