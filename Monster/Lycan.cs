using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public class Lycan : Monster
    {
        int lycanMaxHp = 200;
        int lycanCurrentHp = 200;
        int lycanDmg = 20;

        public Lycan() : base(ENEMY.MONSTER)
        {
            SetInfo(lycanMaxHp, lycanCurrentHp, lycanDmg);
            this.monster = MONSTER.LYCAN;
            ShowAppear();
            ShowStatus();
        }

        public override void ShowAppear()
        {
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\MONSTER\\AppearingLycan.txt", FileMode.Open)))
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
            player.Hit(lycanDmg);
            WriteLine($"\n{GetMob()}이(가) {lycanDmg} 만큼의 피해를 주었다.");
        }
    }
}
