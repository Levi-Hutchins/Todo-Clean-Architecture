using AutoMapper;
using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Mappings;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<Todos, UserTodsDTO>();
    }
}