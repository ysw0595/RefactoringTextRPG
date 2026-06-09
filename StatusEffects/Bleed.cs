using static System.Console;

namespace TextRPG_v2.StatusEffects
{
    public class Bleed : IStatusEffect
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
            if (!player.GetBleed())
            {
                player.RemoveStatusEffect(this);
                return;
            }

            WriteLine($"출혈로 {player.GetBleedCount()}만큼 피해를 입었다.");
            player.Hit(player.GetBleedCount());
            player.SetBleed(-1);
        }
    }
}
