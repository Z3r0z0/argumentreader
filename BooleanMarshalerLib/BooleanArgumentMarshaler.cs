using ArgumentMarshalerLib;
using System;

namespace BooleanMarshalerLib
{
    
    public class BooleanArgumentMarshaler : ArgumentMarshaler
    {
        public override string Schema => "";

        public override void Set(Iterator<string> currentArgument)
        {
            Value = true;
        }

        public class BooleanMarshalerException : ArgumentMarshalerLib.ArgumentException
        {
            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    default:
                        return string.Empty;
                }
            }
        }
    }
    
}
