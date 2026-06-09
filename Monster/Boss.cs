using System;
using System.Collections.Generic;
using TextRPG_v2.Skills;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public class Boss : Monster
    {
        protected readonly List<IAttackPattern> attackPatterns = new List<IAttackPattern>();
        protected string[] skillSet = new string[3];

        public Boss(BOSS boss) : base(ENEMY.BOSS) // 부모(Monster) 생성자 호출용 더미
        {
            this.boss = boss;
        }

        public virtual void SetSkill() { }
        public override MONSTER GetMob() { return MONSTER.NONE; }

        public override BOSS GetBoss()
        {
            return boss;
        }

        public virtual void InvokeAction(string stateName, Player.Player player)
        {
            IAttackPattern? pattern = attackPatterns.Find(attackPattern => attackPattern.Name == stateName);
            pattern?.Execute(this, player);
        }

        public virtual void BossAttack(string skill, ref Player.Player player)
        {
            InvokeAction(skill, player);
        }

        public string GetSkill(int n)
        {
            return skillSet[n];
        }
    }
}
