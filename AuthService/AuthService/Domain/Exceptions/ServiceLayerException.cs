namespace Afonin.AuthService.Domain.Exceptions
{

    [Serializable]
    public class ServiceLayerException : Exception
    {
        public ServiceLayerException() { }
        public ServiceLayerException(string message) : base(message) { }
        public ServiceLayerException(string message, Exception inner) : base(message, inner) { }
        protected ServiceLayerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
