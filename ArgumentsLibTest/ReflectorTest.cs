using ArgumentMarshalerLib;
using ArgumentsLib;
using BooleanMarshalerLib;
using DoubleMarshalerLib;
using IntegerMarschalerLib;
using StringMarshalerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ArgumentsLibTest
{
    public class ReflectorTest
    {
        private Reflector _reflector;

        private static string _directory = @"..\Arguments\Marshaler";

        public ReflectorTest()
        {
            _reflector = new Reflector(_directory);
        }

        [Fact]
        public void CreateReflector_PassingTest()
        {

            Assert.NotNull(_reflector);
        }

        [Fact]
        public void CreateReflector_FailingTest()
        {
            Assert.Throws<LibraryArgumentException>(() => new Reflector(_directory + @"\dirNotExists"));
        }

        [Fact]
        public void GetInstanceBySchemaBool_PassingTest()
        {
            var temp = _reflector.GetInstanceBySchema("");

            Assert.IsType<BooleanArgumentMarshaler>(temp);
        }

        [Fact]
        public void GetInstanceBySchemaInt_PassingTest()
        {
            var temp = _reflector.GetInstanceBySchema("#");

            Assert.IsType<IntegerArgumentMarshaler>(temp);
        }


        [Fact]
        public void GetInstanceBySchemaString_PassingTest()
        {
            var temp = _reflector.GetInstanceBySchema("*");

            Assert.IsType<StringArgumentMarshaler>(temp);
        }

        [Fact]
        public void GetInstanceBySchemaDouble_PassingTest()
        {
            var temp = _reflector.GetInstanceBySchema("##");

            Assert.IsType<DoubleArgumentMarshaler>(temp);
        }

        [Fact]
        public void GetInstanceBySchema_FailingTest()
        {
            Assert.Throws<LibraryArgumentException>(() => _reflector.GetInstanceBySchema(null));
        }
    }
}
