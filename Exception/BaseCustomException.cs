using System.Diagnostics.CodeAnalysis;

namespace Exception
{
    public class BaseCustomException : System.Exception
    {
        public int StatusCode { get; set; }
        public string Messages { get; set; }
        public string Description{ get; set; }
        public object? Error { get; set; }
        public BaseCustomException(int code, string message, string description, object? error = null) : base()
        {
            StatusCode = code;
            Messages = message;
            Description = description;
            Error = error ?? "Something went wrong";
        }

    }
    
    public class Program
    {
        static void Main(string[] args)
        {
        
        }
    }
}