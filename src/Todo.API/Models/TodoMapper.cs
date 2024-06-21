using AutoMapper;
using Todo.API.Models.Requests;
using Todo.API.Models.Responses;

public class TodoMapper : Profile
{
    public TodoMapper() { CreateMap<TodoRequest, TodoResponse>().ReverseMap(); }
}