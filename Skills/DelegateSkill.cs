using System;

namespace TextRPG_v2.Skills
{
    public class DelegateSkill : ISkill
    {
        private readonly Action<Player.Player, Monster.Monster> execute;

        public DelegateSkill(string name, Action<Player.Player, Monster.Monster> execute)
        {
            Name = name;
            this.execute = execute;
        }

        public string Name { get; }

        public void Execute(Player.Player user, Monster.Monster target)
        {
            execute(user, target);
        }
    }
}
