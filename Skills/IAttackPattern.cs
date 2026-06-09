namespace TextRPG_v2.Skills
{
    public interface IAttackPattern
    {
        string Name { get; }
        void Execute(Monster.Monster user, Player.Player target);
    }
}
