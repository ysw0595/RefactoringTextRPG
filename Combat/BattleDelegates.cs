namespace TextRPG_v2.Combat
{
    public delegate void TurnEndedEventHandler(Player.Player player, Monster.Monster monster);
    public delegate void DeathEventHandler(object dead);
}
