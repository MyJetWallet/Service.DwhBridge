using System.ServiceModel;
using System.Threading.Tasks;
using Service.DwhBridge.Grpc.Models;

namespace Service.DwhBridge.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}