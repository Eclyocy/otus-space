
namespace ServiceLayer.Abstractions.ReturnResult
{
    public class FluentError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }

        public FluentError()
        {
                
        }
    }
}
