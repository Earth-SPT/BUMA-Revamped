using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;
using Version = SemanticVersioning.Version;

namespace BUMA_Revamped;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.papershredder432.BumaRevamped2";
    public override string Name { get; init; } = "BUMA Revamped";
    public override string Author { get; init; } = "papershredder432";
    public override List<string>? Contributors { get; init; } = ["MusicManiac", "Mattdokn_", "egbog"];
    public override Version Version { get; init; } = new("2.0.0");
    public override Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; }
    public override string License { get; init; } = "GNU GPLv3";
}