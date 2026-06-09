using System;
using System.Collections.Generic;
using TextRPG_v2.Combat;
using TextRPG_v2.StatusEffects;
using static System.Console;

namespace TextRPG_v2.Player
{
    public enum Character
    {
        NONE,
        VAMPIRE
    }

    public class Player
    {
        protected int maxHp = 0;
        protected int currentHp = 0;
        protected int dmg = 0;
        protected Character character = Character.NONE;

        protected string[] skillSet = new string[6];
        protected int skillCount = 0;

        bool isParalyze = false;
        int paralyzeCount = 0;

        bool addicted = false; // 중독 여부
        int poisonCount = 0;   // 중독 수치

        bool isBleed = false; // 출혈 여부
        int bleedCount = 0;   // 출혈 수치

        private readonly List<IStatusEffect> statusEffects = new List<IStatusEffect>();
        private IStatusEffect? addictedEffect;
        private IStatusEffect? bleedEffect;
        private bool deathRaised = false;

        public event TurnEndedEventHandler? TurnEnded;
        public event DeathEventHandler? Died;

        // 확인을 위해 임시로 protected -> public으로 변경했던 상태
        protected Player(Character character)
        {
            this.character = character;
        }

        protected void SetInfo(int maxHp, int currentHp, int dmg)
        {
            this.maxHp = maxHp;
            this.currentHp = currentHp;
            this.dmg = dmg;
        }

        public int GetMaxHp() { return maxHp; }
        public int GetCurrentHp() { return currentHp; }
        public int GetDmg() { return dmg; }
        public Character GetCharacter() { return character; }
        public virtual void IncreaseMaxHp(int maxHp) { }

        public virtual void GetHeal(int heal){ }

        public virtual void IncreaseDmg(int dmg) { }
        public void ShowStatus()
        {
            WriteLine($"최대 체력 : {maxHp}");
            WriteLine($"현재 체력 : {currentHp}");
            WriteLine($"공격력 : {dmg}\n");
        }

        public int GetSkillCount() { return skillCount; }
        public string GetSkill(int index) { return skillSet[index - 1]; } // 입력값이 1부터 시작하므로 -1 처리
        public virtual void ShowSkill() { }

        public virtual void Attack(string skill, Monster.Monster monster) { }
        public void Hit(int dmg)
        {
            currentHp -= dmg;
            if (currentHp <= 0 && !deathRaised)
            {
                deathRaised = true;
                Died?.Invoke(this);
            }
        }

        public void EndTurn(Monster.Monster monster)
        {
            TurnEnded?.Invoke(this, monster);
        }

        public void ApplyStatusEffect(IStatusEffect statusEffect)
        {
            if (statusEffects.Contains(statusEffect)) return;

            statusEffects.Add(statusEffect);
            statusEffect.Subscribe(this);
        }

        public void RemoveStatusEffect(IStatusEffect statusEffect)
        {
            if (!statusEffects.Remove(statusEffect)) return;

            statusEffect.Unsubscribe(this);
        }

        public virtual void ClearBattleEffects()
        {
            foreach (IStatusEffect statusEffect in statusEffects.ToArray())
            {
                RemoveStatusEffect(statusEffect);
            }

            addictedEffect = null;
            bleedEffect = null;
            poisonCount = 0;
            bleedCount = 0;
        }

        public virtual void SetSkill(int skill) { }
        public virtual bool CheckSkill(int skillName)
        {
            return false;
        }

        public void SetParalyzed(int n)
        {
            paralyzeCount += n;
        }

        public bool IsParalyzed()
        {
            if (paralyzeCount > 0) { isParalyze = true; }
            else isParalyze = false; // 스크린샷과 동일하게 괄호 생략!

            return isParalyze;
        }

        public void SetAddicted(int n) // 중독 상태에 걸리게 할 메서드
        {
            poisonCount += n;
            if (poisonCount < 0) poisonCount = 0;

            if (poisonCount > 0 && addictedEffect == null)
            {
                addictedEffect = new Addicted();
                ApplyStatusEffect(addictedEffect);
            }
            else if (poisonCount == 0 && addictedEffect != null)
            {
                RemoveStatusEffect(addictedEffect);
                addictedEffect = null;
            }
        }

        public int GetPoisonCount() // 턴 종료 수치에 데미지를 주기 위한 메서드
        {
            return poisonCount;
        }

        public bool GetAddicted() // 독의 유무를 판단하기 위한 메서드
        {
            if (poisonCount > 0) { addicted = true; }
            else addicted = false;

            return addicted;
        }

        public int GetBleedCount()
        {
            return bleedCount;
        }

        public void SetBleed(int n)
        {
            bleedCount += n;
            if (bleedCount < 0) bleedCount = 0;

            if (bleedCount > 0 && bleedEffect == null)
            {
                bleedEffect = new Bleed();
                ApplyStatusEffect(bleedEffect);
            }
            else if (bleedCount == 0 && bleedEffect != null)
            {
                RemoveStatusEffect(bleedEffect);
                bleedEffect = null;
            }
        }

        public bool GetBleed()
        {
            if (bleedCount > 0) { isBleed = true; }
            else isBleed = false;

            return isBleed;
        }

        public void Enhance()
        {
            maxHp *= 2;
            currentHp *= 2;
            dmg *= 2;
        }
    }
}
