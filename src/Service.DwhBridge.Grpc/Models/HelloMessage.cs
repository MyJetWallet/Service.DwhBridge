using System.Runtime.Serialization;
using Service.DwhBridge.Domain.Models;

namespace Service.DwhBridge.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}