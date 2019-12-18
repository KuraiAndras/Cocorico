using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Cocorico.Shared.Exceptions
{
    [Serializable]
    public class InvalidCommandException : Exception
    {
        public string? ResourceReferenceProperty { get; set; }

        public InvalidCommandException()
        {
        }

        public InvalidCommandException(string message)
            : base(message)
        {
        }

        public InvalidCommandException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidCommandException(SerializationInfo info, StreamingContext context)
            : base(info, context) =>
            ResourceReferenceProperty = info.GetString(nameof(ResourceReferenceProperty));

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            info.AddValue(nameof(ResourceReferenceProperty), ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}