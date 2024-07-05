using FluentValidation.Results;
using Newtonsoft.Json;


namespace ServiceLayer.Abstractions.ReturnResult
{
    public class ReturnResult<TDto> where TDto : IDTO
    {

        public TDto Model { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<FluentError> Errors { get; set; } = new List<FluentError>();

        public ReturnResult()
        {

        }

        public ReturnResult(TDto dto, List<ValidationFailure> validationFailures = default)
        {
            if (validationFailures != default)
            {
                foreach (var failure in validationFailures)
                {
                    Errors.Add(new FluentError()
                    {
                        ErrorCode = failure.ErrorCode,
                        ErrorMessage = failure.ErrorMessage,
                        PropertyName = failure.PropertyName
                    });
                }
            }
            Model = dto;
        }
    }
}

