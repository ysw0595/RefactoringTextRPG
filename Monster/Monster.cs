using System;
using System.IO;
using TextRPG_v2.Combat;
using static System.Console;

namespace TextRPG_v2.Monster
{
    public enum MONSTER
    {
        NONE,
        FARMER,
        HUNTER,
        WOLF,
        KNIGHT,
        WEREWOLF,
        PALADIN,
        LYCAN
    }
    public enum BOSS
    {
        NONE = 0,
        PRIEST,
        VAN_HELSING,
        WEREFENRIR
    }

    public enum ENEMY
    {
        NONE = 0,
        MONSTER,
        BOSS
    }

    public class Monster
    {
        protected int maxHp = 0;
        protected int currentHp = 0;
        protected int dmg = 0;
        protected MONSTER monster = MONSTER.NONE;
        protected BOSS boss = BOSS.NONE;
        protected ENEMY enemy = ENEMY.NONE;
        private bool deathRaised = false;

        public event DeathEventHandler? Died;

        // 일반 몬스터용 생성자
        public Monster(ENEMY enemy)
        {
            this.enemy = enemy;
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

        public virtual MONSTER GetMob() { return monster; }

        public virtual BOSS GetBoss() { return boss; }

        public void ShowStatus()
        {
            WriteLine($"최대 체력 : {maxHp}");
            WriteLine($"현재 체력 : {currentHp}");
            WriteLine($"공격력 : {dmg}\n");
        }

        public virtual void ShowAppear() { }

        public void ShowDisappear(Player.Player player, MONSTER monster)
        {
            if (monster == MONSTER.FARMER)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingFarmer.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseMaxHp(1);
            }
            else if (monster == MONSTER.HUNTER)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingHunter.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseDmg(1);
            }
            else if (monster == MONSTER.WOLF)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingWolf.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}의 피를 섭취하여");
                player.GetHeal(20);
            }
            else if (monster == MONSTER.KNIGHT)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingKnight.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseMaxHp(10);
            }
            else if (monster == MONSTER.WEREWOLF)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingWereWolf.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseDmg(4);
                Write($"\n{monster}의 피를 섭취하여");
                player.GetHeal(30);
            }
            else if (monster == MONSTER.PALADIN)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingPaladin.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseMaxHp(20);
            }
            else if (monster == MONSTER.LYCAN)
            {
                using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Monster\\DisappearingLycan.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        Write(sr.ReadToEnd());
                    }
                }
                Write($"\n{monster}을(를) 해치워 ");
                player.IncreaseDmg(8);
                Write($"\n{monster}의 피를 섭취하여");
                player.GetHeal(40);
            }
        }

        public void Hit(int dmg)
        {
            currentHp -= dmg;
            if (currentHp <= 0 && !deathRaised)
            {
                deathRaised = true;
                Died?.Invoke(this);
            }
        }

        public ENEMY GetEnemy() { return enemy; }

        public virtual void Attack(Player.Player player) { }
    }
}
