using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagement.Users.Application.Common.GenericValidator;
using TaskManagement.Users.Application.Dto;

namespace TaskManagement.Users.Application.Common.Validations
{
    public class UserDtoValidator : BaseValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().Matches(@"^[a-zA-Z0-9]+$");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                                    .NotNull()
                                    .MinimumLength(8)
                                    .MaximumLength(24)
                                    .Matches(@"[!@#$%^&*(),.?:{}|<>]")
                                    .Matches(@"^\S+$");
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
        }
    }
}