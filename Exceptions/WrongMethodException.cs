using System;

namespace Xunity.Exceptions
{
    public class WrongMethodException : Exception
    {
        readonly string message;

        public override string ToString()
        {
            return message + '\n' + base.ToString();
        }

        public WrongMethodException(string message)
        {
            this.message = message;
        }
    }
}