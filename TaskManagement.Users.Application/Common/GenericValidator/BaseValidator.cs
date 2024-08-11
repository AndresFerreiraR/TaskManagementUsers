using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TaskManagement.Users.Commons.Errors;

namespace TaskManagement.Users.Application.Common.GenericValidator
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public List<BaseError> ValidateAndCollectErrors(T instance)
        {
            var result = Validate(instance);

            if(result.IsValid)
            {
                return new List<BaseError>();
            }

            return result.Errors.Select(e => new BaseError
            {
                PropertyMassage = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            }).ToList();
        }
    }
}