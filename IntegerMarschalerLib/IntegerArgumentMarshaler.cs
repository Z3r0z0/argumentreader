using ArgumentMarshalerLib;
using System;

namespace IntegerMarschalerLib
{
    public class IntegerArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "#";

        public override void Set(Iterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next();
                Value = int.Parse(parameter);
            }
            catch (ArgumentMarshalerLib.ArgumentException)
            {
                throw new IntegerMarshalerException(ErrorCode.MISSING);
            }
            catch (FormatException)
            {
                throw new IntegerMarshalerException(ErrorCode.INVALID, parameter);
            }
            catch(Exception)
            {
                throw new IntegerMarshalerException(ErrorCode.INVALID);
            }
        }

        public class IntegerMarshalerException : ArgumentMarshalerLib.ArgumentException
        {
            public IntegerMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public IntegerMarshalerException(ErrorCode errorCode, string parameter)
            {
                this.ErrorCode = errorCode;
                this.ErrorParameter = parameter;
            }

            public IntegerMarshalerException(ErrorCode errorCode, string argumentId, string parameter) : base(errorCode, argumentId, parameter)
            {
                    
            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find Integer parameter forech -{ErrorArgumentId}";
                    case ErrorCode.INVALID:
                        return $"' -{ErrorArgumentId}' expects an Integer but was {ErrorParameter}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
