using System;
using System.Collections.Generic;
using System.Text;
using TextRPG_v2.Skills;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public enum Van_HelsingSkill
    {
        SILVERARROW,
        CONTINUOUSARROW,
        POISONARROW
    }

    public class Van_Helsing : Boss
    {
        int vanMaxHp = 150;
        int vanCurrentHp = 150;
        int vanDmg = 20;

        public Van_Helsing() : base(BOSS.VAN_HELSING)
        {
            SetInfo(vanMaxHp, vanCurrentHp, vanDmg);
            SetSkill(); // 생성 문구 출력
            ShowAppear();
        }

        public override void ShowAppear()
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Boss\\Van_Helsing.txt", FileMode.Open)))
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
            skillSet[0] = Van_HelsingSkill.SILVERARROW.ToString();
            skillSet[1] = Van_HelsingSkill.CONTINUOUSARROW.ToString();
            skillSet[2] = Van_HelsingSkill.POISONARROW.ToString();

            attackPatterns.Add(new DelegateAttackPattern(skillSet[0], (_, player) => Paralyzed(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[1], (_, player) => General(player)));
            attackPatterns.Add(new DelegateAttackPattern(skillSet[2], (_, player) => Addicted(player)));
        }

        public void General(Player.Player player)
        {
            // 연속 화살: 0.5배 피해를 2번 연속 입힘 (실수형 변환 완벽 적용)
            player.Hit((int)(vanDmg * 0.5f));
            WriteLine($"\n{boss}이(가) {(int)(vanDmg * 0.5f)} 만큼의 피해를 주었다.\n");

            player.Hit((int)(vanDmg * 0.5f));
            WriteLine($"\n{boss}이(가) {(int)(vanDmg * 0.5f)} 만큼의 피해를 주었다.\n");
        }

        public void Paralyzed(Player.Player player)
        {
            player.SetParalyzed(1);
            WriteLine($"\n{boss}이(가) 마비를 1 부여했다.\n");
        }

        public void Addicted(Player.Player player)
        {
            // 독 화살: 0.8배 피해 및 중독 부여
            player.Hit((int)(vanDmg * 0.8f));
            player.SetAddicted(1);
            WriteLine($"\n{boss}이(가) {(int)(vanDmg * 0.8f)} 만큼의 피해를 주고 중독을 부여했다.\n");
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
