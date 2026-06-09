using System;

namespace TextRPG_v2.Monster
{
    public class Farmer : Monster
    {
        int farmerMaxHp = 20;
        int farmerCurrentHp = 20;
        int farmerDmg = 2;

        public Farmer() : base(ENEMY.MONSTER)
        {
            SetInfo(farmerMaxHp, farmerCurrentHp, farmerDmg);
            this.monster = MONSTER.FARMER;
            showAppear();
            ShowStatus();
        }

        public void showAppear()
        {
            // Console.WriteLine($"{GetMob()} 생성!");
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\AppearingFarmer.txt", FileMode.Open)))
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
            player.Hit(farmerDmg);
            Console.WriteLine($"\n{GetMob()}이(가) {farmerDmg} 만큼의 피해를 주었다.");
        }
    }
}