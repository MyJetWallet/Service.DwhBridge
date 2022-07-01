using Autofac;
using Service.DwhBridge.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.DwhBridge.Client
{
    public static class AutofacHelper
    {
        public static void RegisterDwhBridgeClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new DwhBridgeClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
