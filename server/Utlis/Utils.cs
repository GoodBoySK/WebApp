using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace server.Utlis
{
    public static class Utils
    {
        public static ErrorMesage ValidationError(ModelStateDictionary modelState) {
            IEnumerable<ValidationMessage> errors = modelState
                .Where(m => m.Value?.Errors.Any() ?? false)
                .Select(m => new ValidationMessage
                {
                    Field = m.Key,
                    Message = string.Join('\n', m.Value?.Errors.Select(x => x.ErrorMessage) ?? [])
                });
            return new ErrorMesage
            {
                Errors = errors
            };
        }

    }
}