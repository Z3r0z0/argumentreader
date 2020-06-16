using StringMarshalerLib;
using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace StringMarshalerTest
{
    public class StringMarshalerTest
    {
        StringArgumentMarshaler _marshaler;

        public StringMarshalerTest()
        {
            _marshaler = new StringArgumentMarshaler();
        }

        [Fact]
        public void CreateStringMarshalerempty()
        {
            Assert.NotNull(_marshaler);
        }

        [Fact]
        public void GetSchema()
        {
            Assert.Equal("*", _marshaler.Schema);
        }

        [Fact]
        public void GetEmptyValue()
        {
            Assert.Null(_marshaler.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        [InlineData("1234")]
        [InlineData("####")]
        public void SetValue_PassingTest(string paramenter)
        {
            List<string> test = new List<string>();
            test.Add(paramenter);

            _marshaler.Set(new Iterator<string>(test));
            Assert.Equal(paramenter, _marshaler.Value);
        }

        [Fact]
        public void SetValue_FailingTest()
        {
            Assert.Throws<StringArgumentMarshaler.StringMarshalerException>(() => _marshaler.Set(new Iterator<string>(new List<string>())));
        }

        [Theory]
        [InlineData(ErrorCode.MISSING)]
        [InlineData(ErrorCode.INVALID)]
        [InlineData(ErrorCode.GLOBAL)]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME)]
        [InlineData(ErrorCode.INVALID_PARAMETER)]
        [InlineData(ErrorCode.INVALID_SCHEMA)]
        [InlineData(ErrorCode.OK)]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT)]
        public void CreateNewStringMarshalerException(ErrorCode errorCode)
        {
            StringArgumentMarshaler.StringMarshalerException test = new StringArgumentMarshaler.StringMarshalerException(errorCode);

            Assert.NotNull(test);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "Could not find String parameter forech -")]
        [InlineData(ErrorCode.INVALID, "")]
        [InlineData(ErrorCode.GLOBAL, "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "")]
        [InlineData(ErrorCode.OK, "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "")]
        public void StringNewStringMarshalerException_SingleInput(ErrorCode errorCode, string message)
        {
            StringArgumentMarshaler.StringMarshalerException test = new StringArgumentMarshaler.StringMarshalerException(errorCode);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.Null(test.ErrorParameter);
        }
    }
}
