using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using MyJetWallet.Sdk.NoSql;
using MyNoSqlServer.DataReader;
using Service.DwhBridge.Database;
using Service.DwhBridge.Jobs;
using Service.HighYieldEngine.Client;
using Service.HighYieldEngine.Domain.Models.NoSql;

namespace Service.DwhBridge.Modules
{
    public class ServiceModule: Module
    {
        private MyNoSqlTcpClient _myNoSqlClient;
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DwhDbContextFactory>().As<IDwhDbContextFactory>().SingleInstance();

            _myNoSqlClient = (MyNoSqlTcpClient)builder.CreateNoSqlClient(Program.Settings.MyNoSqlReaderHostPort, Program.LogFactory);
            
            builder.RegisterMyNoSqlReader<EarnDashboardNoSqlEntity>(_myNoSqlClient, EarnDashboardNoSqlEntity.TableName);

            builder.RegisterType<EarnJob>().As<IStartable>().AutoActivate().SingleInstance();
        }
    }
}