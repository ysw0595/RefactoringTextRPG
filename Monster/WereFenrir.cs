using System;
using System.Collections.Generic;
using System.Text;
using TextRPG_v2.Skills;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public enum WereFenrirSkill
    {
        MOONHOPER,
        SCRATCH,
        BITE
    }

    public class WereFenrir : Boss
    {
        int wereMaxHp = 350;
        int wereCurrentHp = 350;
        int wereDmg = 30;

        public WereFenrir() : base(BOSS.WEREFENRIR)
        {
            SetInfo(wereMaxHp, wereCurrentHp, wereDmg);
            SetSkill(); // 생성 문구 출력
            ShowAppear();
        }

        public override void ShowAppear()
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Boss\\WereFenrir.txt", FileMode.Open)))
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
            skillSet[0] = WereFenrirSkill.MOONHOPER.ToString();
            skillSet[1] = WereFenrirSkill.SCRATCH.ToString();
            skillSet[2] = WereFenrirSkill.BITE.ToString();

            attackPatterns.Add(new DelegateAttackPattern(skillSet[0], (_, player) => Reinforce(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[1], (_, player) => Bleed(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[2], (_, player) => General(player)));
        }

        public override void InvokeAction(string stateName, Player.Player player)
        {
            base.InvokeAction(stateName, player);
        }

        public override void BossAttack(string skill, ref Player.Player player)
        {
            base.BossAttack(skill, ref player);
        }


        public void Reinforce(Player.Player player)
        {
            wereDmg = (int)(wereDmg * 1.5f); // 공격력 50% 증가)
            WriteLine($"\n{boss}이(가) 달빛 아래에서 공격력이 50% 증가했다.\n");
        }

        public void Bleed(Player.Player player)
        {
            player.Hit((int)(wereDmg * 0.7f));
            player.SetBleed(10); // 10 출혈 적용
            WriteLine($"\n{boss}의 할퀴기 공격에 출혈을 입었다.\n");
        }

        public void General(Player.Player player)
        {
            player.Hit((int)(wereDmg * 0.7f));
            WriteLine($"\n{boss}이(가) {wereDmg * 0.7f} 만큼의 피해를 주었다.\n");
        }
    }
}
