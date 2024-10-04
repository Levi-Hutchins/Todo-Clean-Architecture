using AutoMapper;
using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Mappings;

public class MappingProfile: Profile
{
    // Split this out later Mapper for each data type
    public MappingProfile()
    {
        CreateMap<Todos, TodoDTO>();
        CreateMap<CreateTodoDTO, Todos >();
        CreateMap<UpdateTodoDTO, Todos>();
        CreateMap<Users, UserDTO>();
    }
}