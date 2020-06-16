using ArgumentMarshalerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ArgumentException = ArgumentMarshalerLib.ArgumentException;

namespace ArgumentsLib
{
    public class Argument
    {
        private Dictionary<string, ArgumentMarshaler> _marschalers;
        private Iterator<string> _currentArgument;
        private List<string> _argumentsFound;

        private Reflector _marshalerReflector;

        public Argument(string directory, string schema, IEnumerable<string> args)
        {
            this._marschalers = new Dictionary<string, ArgumentMarshaler>();
            this._argumentsFound = new List<string>();

            _marshalerReflector = new Reflector(directory);

            ParseSchema(schema);
            ParsArgumentStrings(new List<string>(args));

        }

        private void ParseSchema(string schema)
        {
            // "a,b*,c#"

            foreach (string argument in schema.Split(','))
            {
                if (argument.Length > 0 && !string.IsNullOrWhiteSpace(argument))
                {
                    ParseSchemaArgument(argument.Trim());
                }
            }
        }

        private void ParseSchemaArgument(string argument)
        {
            // 1.) a
            // 2.) b*
            // 3.) c#

            string argumentId = string.Empty;

            foreach (char c in argument)
            {
                if (char.IsLetterOrDigit(c))
                {
                    argumentId += c;
                }
                else
                {
                    break;
                }
            }

            // char argumentId = argument.ElementAt(0);    // a, b, c
            string argumentTail = argument.Substring(argumentId.Length); // null, *, #

            this._marschalers.Add(argumentId, _marshalerReflector.GetInstanceBySchema(argumentTail));
        }

        private void ParsArgumentStrings(List<string> argumentList)
        {
            // -a
            // -b
            // Das ist ein Test
            // -c
            // 1234
            for (this._currentArgument = new Iterator<string>(argumentList); this._currentArgument.HasNext;)
            {
                string argumentString = _currentArgument.Next(); // -a

                if (argumentString.Length > 0 && argumentString.ElementAt(0) == '-')
                {
                    ParseArgumentString(argumentString.Substring(1));
                }
                else
                {
                    throw new LibraryArgumentException(ErrorCode.INVALID_PARAMETER, argumentString);
                }
            }
        }


        private void ParseArgumentString(string argumentString)
        {
            if (!this._marschalers.TryGetValue(argumentString, out ArgumentMarshaler m))
                throw new LibraryArgumentException(ErrorCode.UNEXPECTED_ARGUMENT, argumentString, null);

            this._argumentsFound.Add(argumentString);

            try
            {
                m.Set(this._currentArgument);
            }
            catch (ArgumentException exception)
            {
                exception.ErrorArgumentId = argumentString;
                throw exception;
            }
        }

        public T GetValue<T>(string argumnet)
        {
            return this._marschalers.ContainsKey(argumnet) ? this._marschalers[argumnet].Value == null ? default(T) :
                                                            (T)this._marschalers[argumnet].Value :
                                                            throw new LibraryArgumentException(ErrorCode.INVALID_PARAMETER, argumnet);
        }

        public IEnumerable<string> ArgumentsFound => _argumentsFound;
        public IEnumerable<string> Schema
        {
            get
            {
                List<string> schemaList = new List<string>();

                foreach (string item in _marschalers.Keys)
                {
                    schemaList.Add(item);
                }

                return schemaList;
            }
        }
    }
}
