namespace TextRPG_v2.Skills
{
    public interface ISkill
    {
        string Name { get; }
        void Execute(Player.Player user, Monster.Monster target);
    }
}
