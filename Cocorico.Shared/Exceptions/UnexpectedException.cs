using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Cocorico.Shared.Exceptions
{
    [Serializable]
    public class UnexpectedException : Exception
    {
        public string? ResourceReferenceProperty { get; set; }

        public UnexpectedException()
        {
        }

        public UnexpectedException(string message)
            : base(message)
        {
        }

        public UnexpectedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UnexpectedException(SerializationInfo info, StreamingContext context)
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