using System;
using System.Runtime.Serialization;

namespace dotNet5781_02_2427_5101
{
    [Serializable]
    internal class AlreadyInException : Exception
    {
        public AlreadyInException()
        {
        }

        public AlreadyInException(string message) : base(message)
        {
        }

        public AlreadyInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}