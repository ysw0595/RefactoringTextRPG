using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public class Knight : Monster
    {
        int knightMaxHp = 80;
        int knightCurrentHp = 80;
        int knightDmg = 17;

        public Knight() : base(ENEMY.MONSTER)
        {
            SetInfo(knightMaxHp, knightCurrentHp, knightDmg);
            this.monster = MONSTER.KNIGHT;
            ShowAppear();
            ShowStatus();
        }

        public override void ShowAppear()
        {
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\Monster\\AppearingKnight.txt", FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    Write(sr.ReadToEnd());
                }
            }
        }

        public override MONSTER GetMob()
        {
            return monster;
        }

        public override void Attack(Player.Player player)
        {
            player.Hit(knightDmg);
            WriteLine($"\n{GetMob()}이(가) {knightDmg} 만큼의 피해를 주었다.");
        }
    }
}
