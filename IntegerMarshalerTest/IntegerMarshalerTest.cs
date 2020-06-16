using ArgumentMarshalerLib;
using IntegerMarschalerLib;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace IntegerMarshalerTest
{
    public class IntegerMarshalerTest
    {
        IntegerArgumentMarshaler _marshaler;

        // TestConsolenAusgabe
        //private readonly ITestOutputHelper output;

        //public IntegerMarshalerTest(ITestOutputHelper output)
        //{
        //    this.output = output;
        //}

        public IntegerMarshalerTest()
        {
            _marshaler = new IntegerArgumentMarshaler();
        }

        [Fact]
        public void CreateIntegerMarshalerEmpty()
        {
            Assert.NotNull(_marshaler);
        }

        [Fact]
        public void GetSchema()
        {
            Assert.Equal("#", _marshaler.Schema);
        }

        [Fact]
        public void GetEmptyValue()
        {
            Assert.Null(_marshaler.Value);
        }

        [Fact]
        public void SetValue_PassingTest()
        {
            List<string> test = new List<string>();
            test.Add("1234");

            _marshaler.Set(new Iterator<string>(test));

            Assert.Equal(1234, (int)_marshaler.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("2147483649")]
        [InlineData("2.1")]
        [InlineData("3,1415")]
        public void SetValue_FailingTest(string paramenter)
        {
            List<string> test = new List<string>();
            test.Add(paramenter);

            Assert.Throws<IntegerArgumentMarshaler.IntegerMarshalerException>(() => _marshaler.Set(new Iterator<string>(test)));
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
        public void CreateNewIntegerMarshalerException(ErrorCode errorCode)
        {
            IntegerArgumentMarshaler.IntegerMarshalerException test = new IntegerArgumentMarshaler.IntegerMarshalerException(errorCode);

            Assert.NotNull(test);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "Could not find Integer parameter forech -")]
        [InlineData(ErrorCode.INVALID, "' -' expects an Integer but was ")]
        [InlineData(ErrorCode.GLOBAL, "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "")]
        [InlineData(ErrorCode.OK, "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "")]
        public void IntegerMarshalerExceptionGetErrorMessage_SingleInput(ErrorCode errorCode, string message)
        {
            IntegerArgumentMarshaler.IntegerMarshalerException test = new IntegerArgumentMarshaler.IntegerMarshalerException(errorCode);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.Null(test.ErrorParameter);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "a", "Could not find Integer parameter forech -")]
        [InlineData(ErrorCode.INVALID, "a", "' -' expects an Integer but was a")]
        [InlineData(ErrorCode.GLOBAL, "a", "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "a", "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "a", "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "a", "")]
        [InlineData(ErrorCode.OK, "a", "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "a", "")]
        public void IntegerMarshalerExceptionGetErrorMessage_DoubleInput(ErrorCode errorCode, string errorParameter, string message)
        {
            IntegerArgumentMarshaler.IntegerMarshalerException test = new IntegerArgumentMarshaler.IntegerMarshalerException(errorCode, errorParameter);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.NotNull(test.ErrorParameter);

        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "b", "a", "Could not find Integer parameter forech -b")]
        [InlineData(ErrorCode.INVALID, "b", "a", "' -b' expects an Integer but was a")]
        [InlineData(ErrorCode.GLOBAL, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "b", "a", "")]
        [InlineData(ErrorCode.OK, "b", "a", "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "b", "a", "")]
        public void IntegerMarshalerExceptionGetErrorMessage_TrippleInput(ErrorCode errorCode, string errorArgumentId, string errorParameter, string message)
        {
            IntegerArgumentMarshaler.IntegerMarshalerException test = new IntegerArgumentMarshaler.IntegerMarshalerException(errorCode, errorArgumentId, errorParameter);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.NotNull(test.ErrorParameter);

        }
    }
}
