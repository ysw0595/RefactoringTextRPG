using static System.Console;

namespace TextRPG_v2.StatusEffects
{
    public class Addicted : IStatusEffect
    {
        public void Subscribe(Player.Player player)
        {
            player.TurnEnded += OnTurnEnded;
        }

        public void Unsubscribe(Player.Player player)
        {
            player.TurnEnded -= OnTurnEnded;
        }

        private void OnTurnEnded(Player.Player player, Monster.Monster monster)
        {
            if (!player.GetAddicted())
            {
                player.RemoveStatusEffect(this);
                return;
            }

            WriteLine($"중독으로 {player.GetPoisonCount()}만큼 피해를 입었다.");
            player.Hit(player.GetPoisonCount());
            player.SetAddicted(-1);
        }
    }
}
