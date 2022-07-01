using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using MyNoSqlServer.Abstractions;
using Service.DwhBridge.Database;
using Service.DwhBridge.Database.Models;
using Service.HighYieldEngine.Domain.Models.Dtos;
using Service.HighYieldEngine.Domain.Models.NoSql;
using Service.HighYieldEngine.Grpc;

namespace Service.DwhBridge.Jobs
{
    public class EarnJob : IStartable
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private IMyNoSqlServerDataReader<EarnDashboardNoSqlEntity> _myNoSqlServerDataReader;
        private readonly MyTaskTimer _timer;
        private readonly ILogger<EarnJob> _logger;

        public EarnJob(IDwhDbContextFactory dwhDbContextFactory, ILogger<EarnJob> logger,
            IMyNoSqlServerDataReader<EarnDashboardNoSqlEntity> myNoSqlServerDataReader)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
            _logger = logger;
            _myNoSqlServerDataReader = myNoSqlServerDataReader;


            _timer = new MyTaskTimer(nameof(EarnJob), TimeSpan.FromMinutes(1), _logger, DoTime);
        }

        private async Task DoTime()
        {
            if (DateTime.UtcNow.Hour == 00 && DateTime.UtcNow.Minute == 00 ||
                DateTime.UtcNow.Hour == 08 && DateTime.UtcNow.Minute == 00 ||
                DateTime.UtcNow.Hour == 16 && DateTime.UtcNow.Minute == 00)
            {
                await GetEarnDashboard();
            }

            if (DateTime.UtcNow.Hour == 23 && DateTime.UtcNow.Minute == 59)
            {
                await GetEarnDashboardByDay();
            }
        }

        private async Task GetEarnDashboardByDay()
        {
            try
            {
                EarnDashboardNoSqlEntity entity =
                    _myNoSqlServerDataReader.Get(Guid.Empty.ToString()).FirstOrDefault();

                var assets = entity?.AssetInfoItems ?? Array.Empty<EarnDashboardAssetInfoDto>();
                List<EarnDashboardAssetEntityByDay> assetEntities = new List<EarnDashboardAssetEntityByDay>();
                

                foreach (var item in assets)
                {
                    var asset = new EarnDashboardAssetEntityByDay()
                    {
                        AssetSymbol = item.AssetSymbol,
                        ClientInfo = item.ClientInfo,
                        NetInfo = item.NetInfo,
                        SimpleInfo = item.SimpleInfo,
                        TimeStamp = DateTime.UtcNow
                    };
                    
                    assetEntities.Add(asset);
                    
                }
                
                var total = entity?.TotalInfo;

                 
                EarnDashboardTotalEntityByDay  totalEntity = new EarnDashboardTotalEntityByDay()
                    {
                        ClientInfo = total.ClientInfo,
                        NetInfo = total.NetInfo,
                        SimpleInfo = total.SimpleInfo,
                        TimeStamp = DateTime.UtcNow
                    };
                
                

                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.AssetEntitiesByDay.AddRangeAsync(assetEntities);
                await ctx.SaveChangesAsync();
                    
                _logger.LogInformation("GetEarnDashboard added Asset values: {e}",assetEntities.Count);
                
                

                
               await ctx.TotalEntitiesByDay.AddRangeAsync(totalEntity);
               await ctx.SaveChangesAsync();
                    
               _logger.LogInformation("GetEarnDashboard add Total values!");
                

                
            }
            catch (Exception e)
            {
                _logger.LogError($"Error on GetEarnDashboard: {e}");
            }
        }
        
        private async Task GetEarnDashboard()
        {
            try
            {
                EarnDashboardNoSqlEntity entity =
                    _myNoSqlServerDataReader.Get(Guid.Empty.ToString()).FirstOrDefault();

                var assets = entity?.AssetInfoItems ?? Array.Empty<EarnDashboardAssetInfoDto>();
                List<EarnDashboardAssetEntity> assetEntities = new List<EarnDashboardAssetEntity>();
                

                foreach (var item in assets)
                {
                    var asset = new EarnDashboardAssetEntity()
                    {
                        AssetSymbol = item.AssetSymbol,
                        ClientInfo = item.ClientInfo,
                        NetInfo = item.NetInfo,
                        SimpleInfo = item.SimpleInfo,
                        TimeStamp = DateTime.UtcNow
                    };
                    
                    assetEntities.Add(asset);
                    
                }
                
                var total = entity?.TotalInfo;

                EarnDashboardTotalEntity totalEntity = null;

                if (total != null)
                {
                     totalEntity = new EarnDashboardTotalEntity()
                    {
                        ClientInfo = total.ClientInfo,
                        NetInfo = total.NetInfo,
                        SimpleInfo = total.SimpleInfo,
                        TimeStamp = DateTime.UtcNow
                    };
                }
                

                await using var ctx = _dwhDbContextFactory.Create();
                if (assetEntities.Count > 0)
                {
                    await ctx.AssetEntities.AddRangeAsync(assetEntities);
                    await ctx.SaveChangesAsync();
                    
                    _logger.LogInformation("GetEarnDashboard by schedule added Asset values: {e}",assetEntities.Count);
                }
                

                if (totalEntity != null)
                {
                    await ctx.TotalEntities.AddRangeAsync(totalEntity);
                    await ctx.SaveChangesAsync();
                    
                    _logger.LogInformation("GetEarnDashboard by schedule add Total values!");
                }

                
            }
            catch (Exception e)
            {
                _logger.LogError($"Error on GetEarnDashboard by schedule: {e}");
            }
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}