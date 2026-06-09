using System;

namespace TextRPG_v2.Skills
{
    public class DelegateAttackPattern : IAttackPattern
    {
        private readonly Action<Monster.Monster, Player.Player> execute;

        public DelegateAttackPattern(string name, Action<Monster.Monster, Player.Player> execute)
        {
            Name = name;
            this.execute = execute;
        }

        public string Name { get; }

        public void Execute(Monster.Monster user, Player.Player target)
        {
            execute(user, target);
        }
    }
}
