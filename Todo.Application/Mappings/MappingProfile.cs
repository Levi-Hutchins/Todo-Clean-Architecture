using AutoMapper;
using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Models.Todo, TodoDTO>();
        CreateMap<Users, UserDTO>();
    }
}