using System;

namespace TextRPG_v2.Monster
{
    public class Hunter : Monster
    {
        int hunterMaxHp = 25;
        int hunterCurrentHp = 25;
        int hunterDmg = 5;

        public Hunter() : base(ENEMY.MONSTER)
        {
            SetInfo(hunterMaxHp, hunterCurrentHp, hunterDmg);
            this.monster = MONSTER.HUNTER;
            showAppear();
            ShowStatus();
        }

        public void showAppear()
        {
            // Console.WriteLine($"{GetMob()} 생성!");
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\AppearingHunter.txt", FileMode.Open)))
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
            player.Hit(hunterDmg);
            Console.WriteLine($"\n{GetMob()}이(가) {hunterDmg} 만큼의 피해를 주었다.");
        }
    }
}