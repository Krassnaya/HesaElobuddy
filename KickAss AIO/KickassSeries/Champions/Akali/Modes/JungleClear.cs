﻿using System.Linq;
using EloBuddy.SDK;

using Settings = KickassSeries.Champions.Akali.Config.Modes.LaneClear;

namespace KickassSeries.Champions.Akali.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var jgminion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(j => j.Health)
                    .FirstOrDefault(j => j.IsValidTarget(Q.Range));

            if (jgminion == null)return;

            if (Q.IsReady() && jgminion.IsValidTarget(Q.Range) && Settings.UseQ)
            {
                Q.Cast(jgminion);
            }

            if (E.IsReady() && jgminion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast();
            }
        }
    }
}
