using System;
using System.Security.Cryptography.X509Certificates;
using static TextRPG_v2.Player.Vampire;
using TextRPG_v2.Skills;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public enum PriestSkill
    {
        READSPELL,
        CROSS,
        PRAY
    }

    public class Priest : Boss
    {
        int priestMaxHp = 50;
        int priestCurrentHp = 50;
        int priestDmg = 10;

        public Priest() : base(BOSS.PRIEST) // 부모의 보스용 생성자 호출

        {
            SetInfo(priestMaxHp, priestCurrentHp, priestDmg);
            SetSkill(); // 생성 문구 출력
            ShowAppear();
        }

        public override void ShowAppear()
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Boss\\Priest.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine("스토리 파일을 찾을 수 없습니다: " + e.Message);
            }
        }

        public override void SetSkill()
        {
            skillSet[0] = PriestSkill.READSPELL.ToString();
            skillSet[1] = PriestSkill.CROSS.ToString();
            skillSet[2] = PriestSkill.PRAY.ToString();

            attackPatterns.Add(new DelegateAttackPattern(skillSet[0], (_, player) => Paralyzed(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[1], (_, player) => General(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[2], (_, player) => Heal(player)));
        }

        public override BOSS GetBoss() { return boss; }
        public override MONSTER GetMob() { return base.GetMob(); }

        public void General(Player.Player player)
        {
            player.Hit(dmg);
            WriteLine($"{boss}이(가) {dmg} 만큼의 피해를 주었다.\n");
        }

        public void Paralyzed(Player.Player player)
        {
            player.SetParalyzed(1);
            WriteLine($"{boss}이(가) 마비를 1 주었다.\n");
        }

        public void Heal(Player.Player player)
        {
            currentHp += 10;
            if(currentHp > GetMaxHp()) currentHp = GetMaxHp();
            WriteLine($"{boss}이(가) 자신을 10 회복했다.\n");
        }

        public override void BossAttack(string skill, ref Player.Player player)
        {
            base.BossAttack(skill, ref player);
        }

        public override void InvokeAction(string stateName, Player.Player player)
        {
            base.InvokeAction(stateName, player);
        }
    }
}
