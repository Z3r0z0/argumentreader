using ArgumentMarshalerLib;
using System;
using System.Globalization;

namespace DoubleMarshalerLib
{
    public class DoubleArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "##";

        public override void Set(Iterator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                parameter = currentArgument.Next().Replace(',', '.');
                Value = double.Parse(parameter, CultureInfo.InvariantCulture);
            }
            catch (ArgumentMarshalerLib.ArgumentException)
            {
                throw new DoubleMarshalerException(ErrorCode.MISSING);
            }
            catch (FormatException)
            {
                throw new DoubleMarshalerException(ErrorCode.INVALID, parameter);
            }
            catch (Exception)
            {
                throw new DoubleMarshalerException(ErrorCode.INVALID);
            }
        }

        public class DoubleMarshalerException : ArgumentMarshalerLib.ArgumentException
        {
            public DoubleMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public DoubleMarshalerException(ErrorCode errorCode, string parameter)
            {
                this.ErrorCode = errorCode;
                this.ErrorParameter = parameter;
            }

            public DoubleMarshalerException(ErrorCode errorCode, string argumentId, string parameter) : base(errorCode, argumentId, parameter)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find Double parameter forech -{ErrorArgumentId}";
                    case ErrorCode.INVALID:
                        return $"' -{ErrorArgumentId}' expects an Double but was {ErrorParameter}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
