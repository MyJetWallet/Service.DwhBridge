using Service.HighYieldEngine.Domain.Models.Dtos;

namespace Service.DwhBridge.Database.Models;

public class EarnDashboardAssetEntity : EarnDashboardAssetInfoDto
{
    public long Id { get; set; }
    public DateTime TimeStamp { get; set; }

    public static EarnDashboardAssetEntity Create(EarnDashboardAssetInfoDto dto)
    {
        return new EarnDashboardAssetEntity()
        {
            Id = 0,
            TimeStamp = DateTime.UtcNow,
            AssetSymbol = dto.AssetSymbol,
            ClientInfo = dto.ClientInfo,
            NetInfo = dto.NetInfo,
            SimpleInfo = dto.SimpleInfo
        };
    }
}

public class EarnDashboardAssetEntityByDay : EarnDashboardAssetInfoDto
{
    public DateTime TimeStamp { get; set; }

    public static EarnDashboardAssetEntityByDay Create(EarnDashboardAssetInfoDto dto)
    {
        return new EarnDashboardAssetEntityByDay()
        {
            TimeStamp = DateTime.UtcNow,
            AssetSymbol = dto.AssetSymbol,
            ClientInfo = dto.ClientInfo,
            NetInfo = dto.NetInfo,
            SimpleInfo = dto.SimpleInfo
        };
    }
}