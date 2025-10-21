using SPTarkov.Server.Core.Models.Common;

namespace BUMA_Revamped;

public class ModConfig
{
    public List<string> BotsToReplaceAmmoFor { get; set; } = new();
    public Dictionary<string, Dictionary<MongoId, double>> Ammo { get; set; } = new();
}