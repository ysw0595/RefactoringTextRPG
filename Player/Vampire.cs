using System;
using System.Collections.Generic;
using TextRPG_v2.Skills;
using TextRPG_v2.StatusEffects;
using static System.Console;

namespace TextRPG_v2.Player
{
    public enum VampireSkill
    {
        Bat_Fire = 0,
        Blood_Explosion,
        Thirster,
        OverFlow,
        Blood_Diamond
    }

    public class Vampire : Player
    {
        private readonly Dictionary<VampireSkill, ISkill> skillBook = new Dictionary<VampireSkill, ISkill>();
        private readonly List<ISkill> skills = new List<ISkill>();
        private BloodDiamondAura? bloodDiamondAura;

        // Vampire가 가질 스탯 설정
        int vamMaxHp = 100;
        int vamCurrentHp = 100;
        int vamDmg = 300;

        // 부모(Player)의 생성자에 Character.VAMPIRE를 넘겨줌
        public Vampire() : base(Character.VAMPIRE)
        {
            SetInfo(vamMaxHp, vamCurrentHp, vamDmg);
            CreateSkillBook();
            SetSkill((int)VampireSkill.Bat_Fire);
        }

        private void CreateSkillBook()
        {
            skillBook[VampireSkill.Bat_Fire] = new DelegateSkill(VampireSkill.Bat_Fire.ToString(), (_, target) => Bat_Fire(target));
            skillBook[VampireSkill.Blood_Explosion] = new DelegateSkill(VampireSkill.Blood_Explosion.ToString(), (_, target) => Blood_Explosion(target));
            skillBook[VampireSkill.Thirster] = new DelegateSkill(VampireSkill.Thirster.ToString(), (_, target) => Thirster(target));
            skillBook[VampireSkill.OverFlow] = new DelegateSkill(VampireSkill.OverFlow.ToString(), (_, target) => OverFlow(target));
            skillBook[VampireSkill.Blood_Diamond] = new DelegateSkill(VampireSkill.Blood_Diamond.ToString(), (_, target) => Blood_Diamond(target));
        }

        public override void SetSkill(int skill)
        {
            VampireSkill vs = (VampireSkill)skill;
            if (!skillBook.TryGetValue(vs, out ISkill? skillStrategy)) return;

            skills.Add(skillStrategy);
            skillSet[skillCount] = skillStrategy.Name;
            skillCount++;
        }

        public override void Attack(string skill, Monster.Monster monster)
        {
            ISkill? selectedSkill = skills.Find(skillStrategy => skillStrategy.Name == skill);
            selectedSkill?.Execute(this, monster);
        }

        public override void ShowSkill()
        {
            for (int i = 0; i < skillCount; i++)
            {
                // WriteLine이 아니라 Write로 입력창과 한 줄로 연결!
                Write($"[{i + 1}] {skillSet[i]} ");
            }
        }

        private void Bat_Fire(Monster.Monster monster)
        {
            General(monster);
        }

        private void Blood_Explosion(Monster.Monster monster)
        {
            BE(monster);
        }
        private void Thirster(Monster.Monster monster)
        {
            Thirsty(monster);
        }
        private void OverFlow(Monster.Monster monster)
        {
            Overflow(monster);
        }
        private void Blood_Diamond(Monster.Monster monster)
        {
            BD(monster);
        }

        private void General(Monster.Monster monster)
        {
            monster.Hit(dmg);
            WriteLine($"\n{GetCharacter()}이(가) {dmg} 만큼의 피해를 주었다.");
        }

        private void BE(Monster.Monster monster)
        {
            Hit((int)(GetMaxHp() * 0.3));
            monster.Hit((int)(dmg * 2.4));
            Write($"\n{GetCharacter()}이(가) {VampireSkill.Blood_Explosion}로 {(int)(GetMaxHp() * 0.3)} 만큼 체력을 소모하여 {(int)(dmg * 2.4)} 만큼의 피해를 주었다.\n");
        }

        private void Thirsty(Monster.Monster monster)
        {
            monster.Hit((int)(GetDmg() * 0.1));
            Write($"\n{GetCharacter()}이(가) {(int)(GetDmg() * 0.1)} 만큼의 피해를 주었다.\n");
            if(GetCurrentHp() < GetMaxHp())
            {
                Write($"\n흡혈 효과로 ");
                if ((GetCurrentHp() + (int)(GetDmg() * 0.08)) > GetMaxHp())
                {
                    GetHeal(GetMaxHp() - GetCurrentHp());
                }
                else
                {
                    GetHeal((int)(GetDmg() * 0.08));
                }
            }
        }

        private void Overflow(Monster.Monster monster)
        {
            Write($"\n{GetCharacter()}이(가) {VampireSkill.OverFlow}로 영구적으로 ");
            IncreaseDmg(1);
            WriteLine("\n");
        }

        private void BD(Monster.Monster monster)
        {
            if (bloodDiamondAura == null)
            {
                bloodDiamondAura = new BloodDiamondAura();
                ApplyStatusEffect(bloodDiamondAura);
                WriteLine($"{GetCharacter()}이(가) 주변에 {VampireSkill.Blood_Diamond}를 생성했습니다.\n");
            }
            else
            {
                bloodDiamondAura.Strengthen();
                WriteLine($"{GetCharacter()}의 {VampireSkill.Blood_Diamond}가 더 날카로워졌습니다.\n");
            }
        }

        public override void ClearBattleEffects()
        {
            base.ClearBattleEffects();
            bloodDiamondAura = null;
        }

        public override void IncreaseMaxHp(int maxHp)
        {
            this.maxHp += maxHp;
            this.currentHp += maxHp;
            WriteLine($"최대 체력이 {maxHp} 만큼 증가했다.\n");
        }

        public override void GetHeal(int heal)
        {
            currentHp += heal;
            WriteLine($"{heal} 만큼 회복했다.\n");
            if (currentHp > maxHp) currentHp = maxHp;
        }

        public override void IncreaseDmg(int dmg)
        {
            this.dmg += dmg;
            WriteLine($"공격력이 {dmg} 만큼 증가했다.\n");
        }

        public override bool CheckSkill(int skill)
        {
            for (int i = 0; i < skillCount; i++)
            {
                if (skillSet[i] == ((VampireSkill)skill).ToString())
                {
                    return true; 
                }
            }

            return false;
        }
    }
}
