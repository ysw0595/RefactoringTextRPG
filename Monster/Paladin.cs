using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public class Paladin : Monster
    {
        int paladinMaxHp = 150;
        int paladinCurrentHp = 150;
        int paladinDmg = 20;

        public Paladin() : base(ENEMY.MONSTER)
        {
            SetInfo(paladinMaxHp, paladinCurrentHp, paladinDmg);
            this.monster = MONSTER.PALADIN;
            ShowAppear();
            ShowStatus();
        }

        public override void ShowAppear()
        {
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\MONSTER\\AppearingPaladin.txt", FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    WriteLine(sr.ReadLine());
                }
            }
        }

        public override MONSTER GetMob()
        {
            return monster;
        }

        public override void Attack(Player.Player player)
        {
            player.Hit(paladinDmg);
            WriteLine($"\n{GetMob()}이(가) {paladinDmg} 만큼의 피해를 주었다.");
        }
    }
}
