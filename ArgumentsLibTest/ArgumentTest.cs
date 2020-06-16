using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ArgumentsLibTest
{
    public class ArgumentTest
    {
        private static string _directoy = @"..\Arguments\Marshaler";

        [Theory]
        [InlineData("a", new string[] { })]
        [InlineData("port", new string[] { })]
        [InlineData("a*", new string[] { "-a", "Test" })]
        [InlineData("a*", new string[] { "-a", "Test test" })]
        [InlineData("text*", new string[] { "-text", "Test" })]
        [InlineData("text*", new string[] { "-text", "Test test" })]
        [InlineData("Text*", new string[] { "-Text", "Test" })]
        [InlineData("Text*", new string[] { "-Text", "Test test" })]
        [InlineData("a#", new string[] { "-a", "1234" })]
        [InlineData("num#", new string[] { "-num", "1234" })]
        [InlineData("Num#", new string[] { "-Num", "1234" })]
        [InlineData("a, b*, c#", new string[] { "-a", "-b", "Test Test", "-c", "1324456" })]
        [InlineData("a,b*,c#", new string[] { "-a", "-b", "Test Test", "-c", "1324456" })]
        public void CreateArgument_PassingTest(string schema, string[] args)
        {
            Argument arg = new Argument(_directoy, schema, args);

            Assert.NotNull(arg);
        }

        [Theory]
        [InlineData("", new string[] { "Test" })]
        [InlineData("", new string[] { "-Test" })]
        [InlineData("a", new string[] { "-a", "test" })]
        public void CreateArgument_FailingTest(string schema, string[] args)
        {
            Assert.Throws<LibraryArgumentException>(() => new Argument(_directoy, schema, args));
        }

        [Theory]
        [InlineData("a, b, c", new string[] { "-a", "-b", "-c" })]
        [InlineData("test1, test2, test3", new string[] { "-test1", "-test3", "-test2" })]
        [InlineData("test1, test2, test3, test4, test5", new string[] { "-test5", "-test3", "-test1", "-test2", "-test4" })]
        public void GetBoolValue(string schema, string[] args)
        {
            Argument arg = new Argument(_directoy, schema, args);

            string[] arr = schema.Split(',');

            foreach (var item in arr)
            {
                string temp = item.Trim();

                Assert.True(arg.GetValue<bool>(temp));
            }
        }

        [Theory]
        [InlineData("a*", new string[] { "-a", "Test" })]
        [InlineData("a*", new string[] { "-a", "Test test" })]
        [InlineData("text*", new string[] { "-text", "Test" })]
        [InlineData("text*", new string[] { "-text", "Test test" })]
        [InlineData("Text*", new string[] { "-Text", "Test" })]
        [InlineData("Text*", new string[] { "-Text", "Test test" })]
        [InlineData("a*, b*", new string[] { "-a", "Test test", "-b", "Test2 Test2" })]
        [InlineData("test*, testtest*", new string[] { "-test", "Test test", "-testtest", "Test2 Test2" })]
        [InlineData("Test*, TestTest*", new string[] { "-Test", "Test test", "-TestTest", "Test2 Test2" })]
        public void GetStringValue_PassingTest(string schema, string[] args)
        {
            Argument arg = new Argument(_directoy, schema, args);

            string[] arr = schema.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                string trimmedString = arr[i].Trim();
                string key = string.Empty;

                foreach (char c in trimmedString)
                {
                    if (char.IsLetter(c))
                    {
                        key += c;
                    }
                    else
                    {
                        break;
                    }
                }

                Assert.Equal(args[((i * 2) + 1)], arg.GetValue<string>(key));
            }
        }

        [Theory]
        [InlineData("errorTest")]
        [InlineData("")]
        [InlineData("-test")]
        public void GetStringValue_ErrorTest(string errorKeyValue)
        {
            Argument arg = new Argument(_directoy, "test*", new string[] { "-test", "testSring" });

            Assert.Throws<LibraryArgumentException>(() => arg.GetValue<string>(errorKeyValue));
        }

        [Theory]
        [InlineData("a#", new string[] { "-a", "1234" })]
        [InlineData("num#", new string[] { "-num", "1234" })]
        [InlineData("Num#", new string[] { "-Num", "1234" })]
        [InlineData("a#, b#", new string[] {"-a", "278431", "-b", "1324456" })]
        [InlineData("a#,b#,c#", new string[] { "-a", "1337", "-b", "2478431", "-c", "1324456" })]
        public void GetIntegerValue_PassingTest(string schema, string[] args)
        {
            Argument arg = new Argument(_directoy, schema, args);

            string[] arr = schema.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                string trimmedString = arr[i].Trim();
                string key = string.Empty;

                foreach (char c in trimmedString)
                {
                    if (char.IsLetter(c))
                    {
                        key += c;
                    }
                    else
                    {
                        break;
                    }
                }

                Assert.Equal(Convert.ToInt32(args[((i * 2) + 1)]), arg.GetValue<int>(key));
            }
        }

        [Theory]
        [InlineData("error")]
        [InlineData("")]
        [InlineData("-a")]
        public void GetIntegerValue_FailingTest(string errorKeyValue)
        {
            Argument arg = new Argument(_directoy, "a#", new string[] { "-a", "1234" });

            Assert.Throws<LibraryArgumentException>(() => arg.GetValue<int>(errorKeyValue));
        }

    }
}
