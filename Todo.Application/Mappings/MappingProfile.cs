using AutoMapper;
using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Todos, TodoDTO>();
        CreateMap<CreateTodoDTO, Todos >();
        CreateMap<Users, UserDTO>();
    }
}