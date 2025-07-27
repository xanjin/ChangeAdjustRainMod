using UnityEngine;
using Verse;

namespace ChangeAdjustRainMod
{
    public class ChangeAdjustRainMod : Mod
    {
        public static ChangeAdjustRainSettings settings;
        private float lastRainVolume;
        private float lastTorrentialVolume;
        public static bool IsSettingsWindowOpen = false;

        public ChangeAdjustRainMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ChangeAdjustRainSettings>();
            lastRainVolume = settings.rainVolume;
            lastTorrentialVolume = settings.torrentialRainVolume;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            IsSettingsWindowOpen = true;

            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);

            settings.DoWindowContents(inRect); 

            if (settings.rainVolume != lastRainVolume || settings.torrentialRainVolume != lastTorrentialVolume)
            {
                ChangeAdjustRainVolumeComponent.ApplyVolumes();
                lastRainVolume = settings.rainVolume;
                lastTorrentialVolume = settings.torrentialRainVolume;
            }

            IsSettingsWindowOpen = false;
        }

        public override string SettingsCategory() => "Change Adjust Rain Mod";
    }
}
