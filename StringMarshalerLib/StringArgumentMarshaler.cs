using ArgumentMarshalerLib;
using System;

namespace StringMarshalerLib
{
    public class StringArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "*";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                Value = currentArgument.Next();
            }
            catch
            {
                throw new StringMarshalerException(ErrorCode.INVALID_PARAMETER);
            }
        }

        public class StringMarshalerException : ArgumentMarshalerLib.ArgumentException
        {
            public StringMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find String parameter forech -{ErrorArgumentId}";
                    default:
                        return string.Empty;
                }
            }
        }
    }

    
}
