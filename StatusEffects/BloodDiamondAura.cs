using static System.Console;

namespace TextRPG_v2.StatusEffects
{
    public class BloodDiamondAura : IStatusEffect
    {
        private int damage = 1;

        public void Subscribe(Player.Player player)
        {
            player.TurnEnded += OnTurnEnded;
        }

        public void Unsubscribe(Player.Player player)
        {
            player.TurnEnded -= OnTurnEnded;
        }

        public void Strengthen()
        {
            damage++;
        }

        private void OnTurnEnded(Player.Player player, Monster.Monster monster)
        {
            if (monster.GetCurrentHp() <= 0) return;

            monster.Hit(damage);
            WriteLine($"{player.GetCharacter()}의 Blood_Diamond가 {damage}의 피해를 입혔다.");
            damage++;
        }
    }
}
