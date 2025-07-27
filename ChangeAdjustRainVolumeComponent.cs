using RimWorld;
using System.Linq;
using Verse;
using Verse.Sound;

namespace ChangeAdjustRainMod
{
    public class ChangeAdjustRainVolumeComponent : GameComponent
    {
        public ChangeAdjustRainVolumeComponent(Game _) { }

        public override void GameComponentTick()
        {
            // Kann leer bleiben, da wir alles über ApplyVolumes regeln
        }

        public static void ApplyVolumes()
        {
            var rainDef = DefDatabase<SoundDef>.GetNamedSilentFail("Ambient_Rain");
            var torrentialDef = DefDatabase<SoundDef>.GetNamedSilentFail("Ambient_TorrentialRain");

            if (rainDef != null && rainDef.subSounds?.Count > 0)
                rainDef.subSounds[0].volumeRange = new FloatRange(ChangeAdjustRainMod.settings.rainVolume);

            if (torrentialDef != null && torrentialDef.subSounds?.Count > 0)
                torrentialDef.subSounds[0].volumeRange = new FloatRange(ChangeAdjustRainMod.settings.torrentialRainVolume);

            RestartRainSound();
        }
        private static void RestartRainSound()
        {
            var map = Find.CurrentMap;
            if (map == null) return;

            var currentWeather = map.weatherManager.curWeather;
            if (currentWeather == null) return;

            var clearWeather = DefDatabase<WeatherDef>.GetNamedSilentFail("Clear");
            if (clearWeather == null || currentWeather == clearWeather)
            {
                // Kein Umschalten möglich – ggf. einfach Sustainer kicken
                KillActiveSustainers(DefDatabase<SoundDef>.GetNamedSilentFail("Ambient_Rain"));
                KillActiveSustainers(DefDatabase<SoundDef>.GetNamedSilentFail("Ambient_TorrentialRain"));
                return;
            }

            // Wechsel auf Clear und dann sofort zurück
            map.weatherManager.TransitionTo(clearWeather);
            map.weatherManager.TransitionTo(currentWeather);
        }

        private static void KillActiveSustainers(SoundDef def)
        {
            var list = Find.SoundRoot?.sustainerManager?.AllSustainers;
            if (list == null) return;

            foreach (var s in list.ToList())
            {
                if (s.def == def)
                    s.End();
            }
        }
    }
}
