using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.DwhBridge.Settings
{
    public class SettingsModel
    {
        [YamlProperty("DwhBridge.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("DwhBridge.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("DwhBridge.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("DwhBridge.DwhConnectionString")]
        public string DwhConnectionString { get; set; }
        
        [YamlProperty("DwhBridge.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
        
    }
}
