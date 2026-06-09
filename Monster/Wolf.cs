using System;

namespace TextRPG_v2.Monster
{
    public class Wolf : Monster
    {
        int wolfMaxHp = 30;
        int wolfCurrentHp = 30;
        int wolfDmg = 3;

        public Wolf() : base(ENEMY.MONSTER)
        {
            SetInfo(wolfMaxHp, wolfCurrentHp, wolfDmg);
            this.monster = MONSTER.WOLF;
            showAppear();
            ShowStatus();
        }

        public void showAppear()
        {
            // Console.WriteLine($"{GetMob()} 생성!");
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\AppearingWolf.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Console.Write(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("스토리 파일을 찾을 수 없습니다: " + e.Message);
            }
        }

        public override MONSTER GetMob() { return monster; }

        public override void Attack(Player.Player player)
        {
            player.Hit(wolfDmg);
            Console.WriteLine($"\n{GetMob()}이(가) {wolfDmg} 만큼의 피해를 주었다.");
        }
    }
}