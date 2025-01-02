namespace AuthService.Application.ResultBase
{
    public abstract class ResultBase(BaseError? error)
    {
        public bool IsSuccess => Error == null;
        public BaseError? Error { get; } = error;
    }

    public sealed class Result : ResultBase
    {
        private Result() : base(null) { }
        private Result(BaseError error): base(error) { }

        public static Result Success() => new();
        public static Result Failure(BaseError error) => new(error);
    }

    public sealed class Result<T> : ResultBase
    {
        private Result(T Data) : base(null) { this.Data = Data; }
        private Result(BaseError error) : base(error) { }
        public T? Data { get; }

        public static Result<T> Success(T data) => new(data);
        public static Result<T> Failure(BaseError error) => new(error); 
    }
}
