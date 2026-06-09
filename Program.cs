using System;

namespace TextRPG_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임 객체 생성
            Game.Game game = new Game.Game();

            // end가 false가 될 때까지 무한 루프 (FSM 기초)
            while (game.End())
            {
                game.Process();
            }
        }
    }
}