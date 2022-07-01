using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.DwhBridge.Grpc;

namespace Service.DwhBridge.Client
{
    [UsedImplicitly]
    public class DwhBridgeClientFactory: MyGrpcClientFactory
    {
        public DwhBridgeClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
