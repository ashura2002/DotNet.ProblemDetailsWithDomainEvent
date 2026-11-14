
using Application.Common.Errors;

namespace Application.Common.Results
{
    public class ResultWithReturnValue<T> : Result
    {
        public T? Value { get; }


        protected ResultWithReturnValue(bool isSuccess, Error? error, T? value) : base(isSuccess, error)
        {
            Value = value;
        }

        public static ResultWithReturnValue<T> Success(T value)
        {
            return new ResultWithReturnValue<T>(true, null, value);
        }

        public static new ResultWithReturnValue<T> Failure(Error error)
        {
            return new ResultWithReturnValue<T>(false, error, default);
        }
    }
}
