﻿using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ARAMDetFull.Champions
{
    class Taric : Champion
    {

        public Taric()
        {
            ARAMSimulator.champBuild = new Build
            {
                coreItems = new List<ConditionalItem>
                {
                    new ConditionalItem(ItemId.Sunfire_Cape),
                    new ConditionalItem(ItemId.Mercurys_Treads,ItemId.Ninja_Tabi,ItemCondition.ENEMY_AP),
                    new ConditionalItem(ItemId.Spirit_Visage),
                    new ConditionalItem(ItemId.Iceborn_Gauntlet),
                    new ConditionalItem(ItemId.Randuins_Omen),
                    new ConditionalItem(ItemId.Banshees_Veil),
                },
                startingItems = new List<ItemId>
                {
                    ItemId.Giants_Belt
                }
            };
        }

        public override void useQ(Obj_AI_Base target)
        {
            if (!Q.IsReady())
                return;
            var ally =
                ObjectManager.Get<AIHeroClient>().FirstOrDefault(h => h.IsValid && Q.IsInRange(h) && !h.IsDead && h.HealthPercent < 65 && (!h.IsMe || h.HealthPercent < 50));
            if (ally != null && ally.IsInRange(Player.Instance,Q.Range))
                Q.Cast();
        }

        public override void useW(Obj_AI_Base target)
        {
            if (!W.IsReady())
                return;
            if (W.IsReady() && W.IsInRange(target))
            W.Cast();
        }

        public override void useE(Obj_AI_Base target)
        {
            if (!E.IsReady() || W.IsReady())
                return;
            if (E.IsReady())
            E.Cast(target);
        }

        public override void useR(Obj_AI_Base target)
        {
            if (!R.IsReady())
                return;
            if (R.IsReady() && player.CountAllyChampionsInRange(R.Range) >= 1)
            R.Cast();
        }

        public override void setUpSpells()
        {
            Q = new Spell.Active(SpellSlot.Q, 350);
            W = new Spell.Targeted(SpellSlot.W, 800);
            E = new Spell.Skillshot(SpellSlot.E, 580, SkillShotType.Linear, 250, int.MaxValue, 140);
            R = new Spell.Active(SpellSlot.R, 400);
        }

        public override void useSpells()
        {
            var tar = ARAMTargetSelector.getBestTarget(Q.Range);
            if (tar != null) useQ(tar);
            tar = ARAMTargetSelector.getBestTarget(W.Range);
            if (tar != null) useW(tar);
            tar = ARAMTargetSelector.getBestTarget(E.Range);
            if (tar != null) useE(tar);
            tar = ARAMTargetSelector.getBestTarget(R.Range);
            if (tar != null) useR(tar);
        }
    }
}