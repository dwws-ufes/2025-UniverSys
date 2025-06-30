using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace UniverSys.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(ValidationFailure failure)
            : this()
        {
            string[] errors = { failure.ErrorMessage };

            Errors = new List<ValidationFailure>
            {
                failure
            }.ToDictionary(x => x.PropertyName, x => errors);
        }

        public ValidationException(string propertyName, string errorMessage)
            : this()
        {
            string[] errors = { errorMessage };

            Errors = new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, errorMessage)
            }.ToDictionary(x => x.PropertyName, x => errors);
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}