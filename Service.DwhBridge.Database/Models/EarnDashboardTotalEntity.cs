using Service.HighYieldEngine.Domain.Models.Dtos;

namespace Service.DwhBridge.Database.Models;

public class EarnDashboardTotalEntity : EarnDashboardTotalInfoDto
{
    public long Id { get; set; }
    public DateTime TimeStamp { get; set; }

    public static EarnDashboardTotalEntity Create(EarnDashboardTotalInfoDto dto)
    {
        return new EarnDashboardTotalEntity()
        {
            Id = 0,
            TimeStamp = DateTime.UtcNow,
            ClientInfo = dto.ClientInfo,
            NetInfo = dto.NetInfo,
            SimpleInfo = dto.SimpleInfo
        };
    }
}

public class EarnDashboardTotalEntityByDay : EarnDashboardTotalInfoDto
{
    public DateTime TimeStamp { get; set; }

    public static EarnDashboardTotalEntityByDay Create(EarnDashboardTotalInfoDto dto)
    {
        return new EarnDashboardTotalEntityByDay()
        {
            TimeStamp = DateTime.UtcNow,
            ClientInfo = dto.ClientInfo,
            NetInfo = dto.NetInfo,
            SimpleInfo = dto.SimpleInfo
        };
    }
}