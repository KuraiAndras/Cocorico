using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Cocorico.Shared.Exceptions
{
    [Serializable]
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message)
            : base(message)
        {
        }

        public InvalidCredentialsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context)
            : base(info, context) =>
            ResourceReferenceProperty = info.GetString(nameof(ResourceReferenceProperty));

        public string? ResourceReferenceProperty { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            info.AddValue(nameof(ResourceReferenceProperty), ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
