﻿using System.Collections.Generic;
using System.Linq;

namespace ARAMDetFull
{
    public class Aggresivity
    {
        private static List<AgresiveMove> agresiveMoves = new List<AgresiveMove>();

        public static void addAgresiveMove(AgresiveMove move)
        {
            if(!move.oneTimeUse || !getOneTime())
                agresiveMoves.Add(move);
        }

        public static int getAgroBalance()
        {
            agresiveMoves.RemoveAll(mov => mov.endAt < ARAMDetFull.now);
            if (!agresiveMoves.Any())
                return 0;
            return agresiveMoves.Max(agr => agr.agroBalance);
        }

        public static bool getIgnoreMinions()
        {
            agresiveMoves.RemoveAll(mov => mov.endAt < ARAMDetFull.now);
            
            return agresiveMoves.Any(agr => agr.ignoreMinions);
        }

        public static bool getOneTime()
        {
            agresiveMoves.RemoveAll(mov => mov.endAt < ARAMDetFull.now);

            return agresiveMoves.Any(agr => agr.oneTimeUse);
        }
    }

    public class AgresiveMove
    {
        public int endAt;
        public int agroBalance;
        public bool ignoreMinions;
        public bool oneTimeUse;
        public AgresiveMove(int agro = 10, int duration = 5000, bool ignoreMins = false, bool oneTime = false)
        {
            agroBalance = 10;
            endAt = ARAMDetFull.now + duration;
            ignoreMinions = ignoreMins;
            oneTimeUse = oneTime;
        }
    }
}