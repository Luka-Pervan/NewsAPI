namespace NewsAPI.Shared
{
    // Non-generic Result class to encapsulate a simple success/failure result
    public class Result
    {
        // Public getter, protected setter to restrict modification from outside
        public bool Succeeded { get; protected set; }
        public string ErrorMessage { get; protected set; }

        // Factory method for a successful result
        public static Result Success()
        {
            return new Result { Succeeded = true };
        }

        // Factory method for a failed result
        public static Result Failure(string errorMessage)
        {
            return new Result { Succeeded = false, ErrorMessage = errorMessage };
        }
    }

    // Generic Result class to encapsulate a result with data (T) in addition to success/failure
    public class Result<T> : Result
    {
        public T Data { get; private set; }

        // Factory method for a successful result with data
        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        // Factory method for a failed result with an error message
        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                Succeeded = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
