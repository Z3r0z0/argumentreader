using System;
using Xunit;
using ArgumentsLib;
using ArgumentMarshalerLib;

namespace ArgumentsLibTest
{
    public class LibraryArgumentExceptionTest
    {
        [Fact]
        public void CreateLibraryArgumentException1()
        {
            LibraryArgumentException ex = new LibraryArgumentException(ErrorCode.OK, string.Empty);

            Assert.NotNull(ex);
        }

        [Fact]
        public void CreateLibraryArgumentException2()
        {
            LibraryArgumentException ex = new LibraryArgumentException(ErrorCode.OK, string.Empty, string.Empty);

            Assert.NotNull(ex);
        }

        [Theory]
        [InlineData(ErrorCode.OK, "TestErrorArgument", "TestErrorArgumentId", "TILT: Should not be reached!")]
        [InlineData(ErrorCode.UNEXPECTED_ARGUMENT, "TestErrorArgument", "TestErrorArgumentId", "Argument -TestErrorArgument unexpected")]
        [InlineData(ErrorCode.INVALID_ARGUMENT_NAME, "TestErrorArgument", "TestErrorArgumentId", "' -TestErrorArgument' is not a valid argument name")]
        [InlineData(ErrorCode.INVALID_PARAMETER, "TestErrorArgument", "TestErrorArgumentId", "' -TestErrorArgumentId is not a valid parameter'")]
        public void GetErrorMessage_PassingTest(ErrorCode code, string argument, string argumentId, string errorText)
        {
            LibraryArgumentException ex = new LibraryArgumentException(code, argument, argumentId);

            Assert.Equal(errorText, ex.ErrorMessage());
        }

        [Theory]
        [InlineData(ErrorCode.GLOBAL, "TestErrorArgument", "TestErrorArgumentId")]
        [InlineData(ErrorCode.INVALID, "TestErrorArgument", "TestErrorArgumentId")]
        [InlineData(ErrorCode.MISSING, "TestErrorArgument", "TestErrorArgumentId")]
        public void GetErrorMessage_FailingTest(ErrorCode code, string argument, string argumentId)
        {
            LibraryArgumentException ex = new LibraryArgumentException(code, argument, argumentId);

            Assert.Equal(string.Empty, ex.ErrorMessage());
        }
    }
}
