using System;
using System.Runtime.Serialization;

namespace dotNet5781_02_2427_5101
{
    [Serializable]
    internal class NoLineException : Exception
    {
        public NoLineException()
        {
        }

        public NoLineException(string message) : base(message)
        {
        }

        public NoLineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}