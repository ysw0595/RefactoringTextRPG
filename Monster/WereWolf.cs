using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public class Werewolf : Monster
    {
        int werewolfMaxHp = 60;
        int werewolfCurrentHp = 60;
        int werewolfDmg = 15;

        public Werewolf() : base(ENEMY.MONSTER)
        {
            SetInfo(werewolfMaxHp, werewolfCurrentHp, werewolfDmg);
            this.monster = MONSTER.WEREWOLF;
            ShowAppear();
            ShowStatus();
        }

        public override void ShowAppear()
        {
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\Monster\\AppearingWereWolf.txt", FileMode.Open)))
            {
                while (!sr.EndOfStream) { Write(sr.ReadToEnd()); }
            }
        }

        public override MONSTER GetMob() { return monster; }

        public override void Attack(Player.Player player)
        {
            player.Hit(werewolfDmg);
            WriteLine($"\n{GetMob()}이(가) {werewolfDmg} 만큼의 피해를 주었다.");
        }
    }
}
