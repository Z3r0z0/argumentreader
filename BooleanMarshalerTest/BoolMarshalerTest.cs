using System;
using BooleanMarshalerLib;
using ArgumentMarshalerLib;
using Xunit;
using System.Collections.Generic;

namespace BooleanMarshalerTest
{
    public class BoolMarshalerTest
    {
        private BooleanArgumentMarshaler _marshaler;

        public BoolMarshalerTest()
        {
            _marshaler = new BooleanArgumentMarshaler();
        }

        [Fact]
        public void CreateBooleanMarshalerEmpty()
        {
            Assert.NotNull(_marshaler);
        }

        [Fact]
        public void GetSchema()
        {
            Assert.Equal("" ,_marshaler.Schema);
        }

        [Fact]
        public void GetEmptyValue()
        {
            Assert.Null(_marshaler.Value);
        }

        [Fact]
        public void SetValue()
        {
            _marshaler.Set(new Iterator<string>(new List<string>()));

            Assert.True((bool)_marshaler.Value);
        }

        [Fact]
        public void CreateNewBooleanMarshalerException()
        {
            BooleanArgumentMarshaler.BooleanMarshalerException test = new BooleanArgumentMarshaler.BooleanMarshalerException();

            Assert.NotNull(test);
        }

        [Fact]
        public void BooleanMarshalerExceptionGetErrorMessage()
        {
            BooleanArgumentMarshaler.BooleanMarshalerException test = new BooleanArgumentMarshaler.BooleanMarshalerException();

            Assert.Equal(string.Empty, test.ErrorMessage());
        }

    }
}
