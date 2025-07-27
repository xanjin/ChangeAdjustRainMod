using Verse;
using UnityEngine;

namespace ChangeAdjustRainMod
{
    public class ChangeAdjustRainSettings : ModSettings
    {
        public float rainVolume = 12f;
        public float torrentialRainVolume = 12f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref rainVolume, "rainVolume", 12f);
            Scribe_Values.Look(ref torrentialRainVolume, "torrentialRainVolume", 12f);
        }

        public void DoWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label($"Rain Volume: {rainVolume:F1}");
            rainVolume = listing.Slider(rainVolume, 0f, 40f);

            listing.Gap(10);

            listing.Label($"Torrential Rain Volume: {torrentialRainVolume:F1}");
            torrentialRainVolume = listing.Slider(torrentialRainVolume, 0f, 40f);

            listing.End();
        }
    }
}
