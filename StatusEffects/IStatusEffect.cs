namespace TextRPG_v2.StatusEffects
{
    public interface IStatusEffect
    {
        void Subscribe(Player.Player player);
        void Unsubscribe(Player.Player player);
    }
}
