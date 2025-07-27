using ChangeAdjustRainMod;
using UnityEngine;
using Verse;

namespace ChangeAdjustRainMod
{
    public class ChangeAdjustRainMod : Mod
    {
        public static ChangeAdjustRainSettings settings;
        private float lastRainVolume;
        private float lastTorrentialVolume;

        public ChangeAdjustRainMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ChangeAdjustRainSettings>();
            lastRainVolume = settings.rainVolume;
            lastTorrentialVolume = settings.torrentialRainVolume;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);

            list.Label($"Rain Volume: {settings.rainVolume:F1}");
            settings.rainVolume = list.Slider(settings.rainVolume, 0f, 50f);

            list.Label($"Torrential Rain Volume: {settings.torrentialRainVolume:F1}");
            settings.torrentialRainVolume = list.Slider(settings.torrentialRainVolume, 0f, 50f);

            list.End();

            // Wenn sich ein Wert ändert, sofort aktualisieren
            if (settings.rainVolume != lastRainVolume || settings.torrentialRainVolume != lastTorrentialVolume)
            {
                ChangeAdjustRainVolumeComponent.ApplyVolumes();
                lastRainVolume = settings.rainVolume;
                lastTorrentialVolume = settings.torrentialRainVolume;
            }
        }

        public override string SettingsCategory() => "Change Adjust Rain Mod";
    }
}
