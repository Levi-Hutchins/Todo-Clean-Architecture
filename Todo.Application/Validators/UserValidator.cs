using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Validators;
using FluentValidation;



public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required.");

    }
}
