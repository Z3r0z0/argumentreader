using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentMarshalerLib
{
    public enum ErrorCode
    {
        OK,
        UNEXPECTED_ARGUMENT,
        MISSING,
        INVALID,
        INVALID_PARAMETER,
        INVALID_ARGUMENT_NAME,
        INVALID_SCHEMA,
        GLOBAL
    }

    public abstract class ArgumentException : Exception
    {
        public ArgumentException()
        {

        }

        public ArgumentException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
        
        public ArgumentException(string message) : base(message)
        {

        }

        public ArgumentException(string errorArgumentId, string errorParmeter)
        {
            this.ErrorArgumentId = errorArgumentId;
            this.ErrorParameter = errorParmeter;
        }

        public ArgumentException(ErrorCode errorCode ,string errorArgumentId, string errorParmeter)
        {
            this.ErrorCode = errorCode;
            this.ErrorArgumentId = errorArgumentId;
            this.ErrorParameter = errorParmeter;
        }

        public string ErrorArgumentId { get; set; }
        public string ErrorParameter { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public abstract string ErrorMessage();
    }
}
