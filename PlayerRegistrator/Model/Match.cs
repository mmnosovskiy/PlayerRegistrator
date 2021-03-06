﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PlayerRegistrator
{
    public class Match
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public int Half { get; set; } //Номер тайма (1..2)
        public int TimeVideo { get; set; } //Текущая секунда времени видео
        public List<Player> DisabledPlayers { get; set; }
        public Tactics GetCurrent(Team team)
        {
            var seq = from tactics in team.Tactics
                      where (tactics.TimeVideo > TimeVideo - 1000 || tactics.TimeVideo > TimeVideo + 1000) && tactics.Half == Half
                      select tactics;
            List<Tactics> list = seq.ToList();
            if (list.Count > 0)
            {
                return list[0];
            }
            else throw new Exception();
        }
        // получить текущую расстановку

    }
}
