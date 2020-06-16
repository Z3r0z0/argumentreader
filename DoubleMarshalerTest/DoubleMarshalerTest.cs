using DoubleMarshalerLib;
using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Xunit;
using System.Reflection;

namespace DoubleMarshalerTest
{
    public class DoubleMarshalerTest
    {
        DoubleArgumentMarshaler _marshaler;

        public DoubleMarshalerTest()
        {
            _marshaler = new DoubleArgumentMarshaler();
        }

        [Fact]
        public void CreateDoubleMarshalerEmpty()
        {
            Assert.NotNull(_marshaler);
        }

        [Fact]
        public void GetSchema()
        {
            Assert.Equal("##", _marshaler.Schema);
        }

        [Fact]
        public void GetEmptyValue()
        {
            Assert.Null(_marshaler.Value);
        }

        [Theory]
        [InlineData("1324")]
        [InlineData("12,34")]
        [InlineData("3.1415")]
        public void SetValue_PassingTest(string parameter)
        {
            List<string> test = new List<string>();
            test.Add(parameter);

            _marshaler.Set(new Iterator<string>(test));

            parameter = parameter.Replace(',', '.');

            Assert.Equal(double.Parse(parameter, CultureInfo.InvariantCulture), _marshaler.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        [InlineData("####")]
        public void SetValue_FailingTest(string paramenter)
        {
            List<string> test = new List<string>();
            test.Add(paramenter);

            Assert.Throws<DoubleArgumentMarshaler.DoubleMarshalerException>(() => _marshaler.Set(new Iterator<string>(test)));
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
        public void CreateNewDoubleMarshalerException(ErrorCode errorCode)
        {
            DoubleArgumentMarshaler.DoubleMarshalerException test = new DoubleArgumentMarshaler.DoubleMarshalerException(errorCode);

            Assert.NotNull(test);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "Could not find Double parameter forech -")]
        [InlineData(ErrorCode.INVALID, "' -' expects an Double but was ")]
        [InlineData(ErrorCode.GLOBAL, "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "")]
        [InlineData(ErrorCode.OK, "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "")]
        public void DoubleMarshalerExceptionGetErrorMessage_SingleInput(ErrorCode errorCode, string message)
        {
            DoubleArgumentMarshaler.DoubleMarshalerException test = new DoubleArgumentMarshaler.DoubleMarshalerException(errorCode);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.Null(test.ErrorParameter);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "a", "Could not find Double parameter forech -")]
        [InlineData(ErrorCode.INVALID, "a", "' -' expects an Double but was a")]
        [InlineData(ErrorCode.GLOBAL, "a", "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "a", "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "a", "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "a", "")]
        [InlineData(ErrorCode.OK, "a", "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "a", "")]
        public void IntegerMarshalerExceptionGetErrorMessage_DoubleInput(ErrorCode errorCode, string errorParameter, string message)
        {
            DoubleArgumentMarshaler.DoubleMarshalerException test = new DoubleArgumentMarshaler.DoubleMarshalerException(errorCode, errorParameter);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.NotNull(test.ErrorParameter);
        }

        [Theory]
        [InlineData(ErrorCode.MISSING, "b", "a", "Could not find Double parameter forech -b")]
        [InlineData(ErrorCode.INVALID, "b", "a", "' -b' expects an Double but was a")]
        [InlineData(ErrorCode.GLOBAL, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "b", "a", "")]
        [InlineData(ErrorCode.INVALID_SCHEMA, "b", "a", "")]
        [InlineData(ErrorCode.OK, "b", "a", "")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "b", "a", "")]
        public void IntegerMarshalerExceptionGetErrorMessage_TrippleInput(ErrorCode errorCode, string errorArgumentId, string errorParameter, string message)
        {
            DoubleArgumentMarshaler.DoubleMarshalerException test = new DoubleArgumentMarshaler.DoubleMarshalerException(errorCode, errorArgumentId, errorParameter);

            Assert.Equal(message, test.ErrorMessage());
            Assert.Equal(errorCode, test.ErrorCode);
            Assert.NotNull(test.ErrorParameter);

        }
    }
}
