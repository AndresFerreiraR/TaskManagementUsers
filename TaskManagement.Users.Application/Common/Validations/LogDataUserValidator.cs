using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagement.Users.Application.Common.GenericValidator;
using TaskManagement.Users.Application.Dto;

namespace TaskManagement.Users.Application.Common.Validations
{
    public class LogDataUserValidator : BaseValidator<LogDataUserDto>
    {

        public LogDataUserValidator()
        {
            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("El password no puede estar vacío")
            .NotNull().WithMessage("El password no puede ser nulo");

            RuleFor(user => user)
                .Must(HaveEitherEmailOrUserName)
                .WithMessage("Debe proporcionar un UserName o un Email");

            RuleFor(user => user.UserName)
                .Empty()
                .When(user => !string.IsNullOrEmpty(user.UserEmail))
                .WithMessage("No puede proporcionar UserName si ya proporcionó Email");

            RuleFor(user => user.UserEmail)
                .Empty()
                .When(user => !string.IsNullOrEmpty(user.UserName))
                .WithMessage("No puede proporcionar Email si ya proporcionó UserName");
        }

        private bool HaveEitherEmailOrUserName(LogDataUserDto user)
        {
            return !string.IsNullOrEmpty(user.UserEmail) || !string.IsNullOrEmpty(user.UserName);
        }
    }
}