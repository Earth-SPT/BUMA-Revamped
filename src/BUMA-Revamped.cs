using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Spt.Logging;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Servers;

namespace BUMA_Revamped;

[Injectable (TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class BumaRevamped(DatabaseServer databaseServer, ISptLogger<BumaRevamped> logger, ModHelper modHelper) : IOnLoad
{
    private ModConfig _config;
    
    public Task OnLoad()
    {
        var itemsDb = databaseServer.GetTables().Templates.Items;
        var botsDb = databaseServer.GetTables().Bots.Types;
        
        try
        {
            var modPath = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
            var configPath = Path.Combine(modPath, "config");
            _config = modHelper.GetJsonDataFromFile<ModConfig>(configPath, "config.json");

            if (_config == null)
            {
                logger.Error("[BUMA-Revamped] Config file could not be loaded or is empty.");
                return Task.CompletedTask;
            }
            
        }
        catch (Exception e)
        {
            logger.Error($"[BUMA-Revamped] Could not load config, mod changes will not apply. Please run your config through a validator if all bot types and ammo types are correct.");
            logger.Error(e.Message);
        }
        
        foreach (var bot in botsDb)
        {
            if (!_config.BotsToReplaceAmmoFor.Contains(bot.Key))
                continue;

            
            
            foreach (var ammo in bot.Value.BotInventory.Ammo)
            {
                if (!_config.Ammo.ContainsKey(ammo.Key))
                    continue;
                
                bot.Value.BotInventory.Ammo[ammo.Key] = _config.Ammo[ammo.Key];
            }
        }
        
        logger.Log(LogLevel.Info, $"[BUMA-Revamped] Changed the ammo pool on {_config.BotsToReplaceAmmoFor.Count} bots. Enjoy getting your armor shredded, or just dying.", LogTextColor.Cyan);
        
        return Task.CompletedTask;
    }
}