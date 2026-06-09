using TextRPG_v2.Monster;
using TextRPG_v2.Player;
using System;
using System.Collections;
using System.IO; // Day 3 핵심: 파일 입출력을 위한 네임스페이스 추가!
using System.Security.Principal;
using static System.Console;

namespace TextRPG_v2.Game
{
    public enum Chapter
    {
        LOBBY = 0,
        CHAPTER1,
        CHAPTER2,
        CHAPTER3,
        OVER,
        ENDING
    }

    public class Game
    {
        Chapter chapter = Chapter.LOBBY;
        Player.Player player;
        bool end = true;
        int turn;
        string next;
        Random random = new Random();

        public void Process()
        {
            switch (chapter)
            {
                case Chapter.LOBBY:
                    Lobby();
                    break;
                case Chapter.CHAPTER1: // 챕터 1 케이스 추가!
                    Chapter1();
                    break;
                case Chapter.CHAPTER2: // 챕터 1 케이스 추가!
                    Chapter2();
                    break;
                case Chapter.CHAPTER3: // 챕터 1 케이스 추가!
                    Chapter3();
                    break;
                case Chapter.OVER:
                    break;
            }
        }

        public void Lobby()
        {
            WriteLine("    IF THE NIGHT COMES\n");

            while (true)
            {
                WriteLine("\n캐릭터를 선택하세요.");
                Write("[1] 뱀파이어 : ");

                player = null;
                end = true;
                turn = 1;

                string input = ReadLine();

                switch (input)
                {
                    case "1":
                        WriteLine("\n뱀파이어를 선택하셨습니다.\n");
                        player = new Player.Vampire();
                        player.Died += OnPlayerDied;
                        chapter = Chapter.CHAPTER1; // 챕터 1로 상태 전이
                        ConsoleClear();
                        break;
                }

                if (chapter == Chapter.CHAPTER1) break;
            }
        }

        // --- Day 3 핵심: 챕터 1 진행 루프 ---
        public void Chapter1()
        {
            int ranMob = random.Next((int)Monster.MONSTER.FARMER, (int)Monster.MONSTER.WOLF + 1);

            for (int i = 0; i < 10; i++)
            {
                WriteLine($"\n챕터 {(int)chapter} : 폭풍전야 (stage {i + 1})");
                switch (i)
                {
                    case 0:
                        ShowPrologue(chapter);
                        break;
                    case 5:
                        ShowEpisode(chapter);
                        break;
                    case 9:
                        FaceBoss(chapter);
                        if (chapter != Chapter.OVER)
                        {
                            ShowEpilogue(chapter);
                            SetSkill(player);
                            Initialization(player);
                            chapter++;
                        }
                        ShowEpilogue(chapter);
                        SetSkill(player);
                            chapter++;
                        break;
                    default:
                        FaceMonster(chapter, ref ranMob, ref i);
                        break;
                }
                if (chapter == Chapter.OVER) { break; }
            }
        }

        public void Chapter2()
        {
            int ranMob = random.Next((int)Monster.MONSTER.WOLF, (int)Monster.MONSTER.WEREWOLF + 1);

            for (int i = 0; i < 10; i++)
            {
                WriteLine($"\n챕터 {(int)chapter} : 붉은 숲 (stage {i + 1})");
                switch (i)
                {
                    case 0:
                        ShowPrologue(chapter);
                        break;
                    case 5:
                        ShowEpisode(chapter);
                        break;
                    case 9:
                        FaceBoss(chapter);
                        if (chapter != Chapter.OVER)
                        {
                            ShowEpilogue(chapter);
                            SetSkill(player);
                            Initialization(player);
                            chapter++;
                        }
                        break;
                    default:
                        FaceMonster(chapter, ref ranMob, ref i);
                        break;
                }
                if (chapter == Chapter.OVER) { break; }
            }
        }

        public void Chapter3()
        {
            int ranMob = random.Next((int)Monster.MONSTER.WEREWOLF, (int)Monster.MONSTER.LYCAN + 1);

            for (int i = 0; i < 10; i++)
            {
                WriteLine($"\n챕터 {(int)chapter} : 적만월(赤滿月) (stage {i + 1})");
                switch (i)
                {
                    case 0:
                        ShowPrologue(chapter);
                        break;
                    case 5:
                        ShowEpisode(chapter);
                        break;
                    case 9:
                        FaceBoss(chapter);
                        if (chapter != Chapter.OVER)
                        {
                            ShowEpilogue(chapter);
                            SetSkill(player);
                            Initialization(player);
                            chapter++;
                        }
                        break;
                    default:
                        FaceMonster(chapter, ref ranMob, ref i);
                        break;
                }
                if (chapter == Chapter.OVER) { break; }
            }
        }

        private void ShowState(Player.Player player, Monster.Monster monster, int turn)
        {
            if (monster.GetEnemy() == ENEMY.MONSTER)
            {
                WriteLine($"\n현재 턴 : {turn}");
                WriteLine($"{player.GetCharacter()}의 최대 체력 : {player.GetMaxHp()} \t {monster.GetMob()}의 최대 체력 : {monster.GetMaxHp()}");
                WriteLine($"{player.GetCharacter()}의 현재 체력 : {player.GetCurrentHp()} \t {monster.GetMob()}의 현재 체력 : {monster.GetCurrentHp()}");
                WriteLine($"{player.GetCharacter()}의 공격력 : {player.GetDmg()} \t {monster.GetMob()}의 공격력 : {monster.GetDmg()}");
            }
            else if (monster.GetEnemy() == ENEMY.BOSS)
            {
                WriteLine($"\n현재 턴 : {turn}");
                WriteLine($"{player.GetCharacter()}의 최대 체력 : {player.GetMaxHp()} \t {monster.GetBoss()}의 최대 체력 : {monster.GetMaxHp()}");
                WriteLine($"{player.GetCharacter()}의 현재 체력 : {player.GetCurrentHp()} \t {monster.GetBoss()}의 현재 체력 : {monster.GetCurrentHp()}");
                WriteLine($"{player.GetCharacter()}의 공격력 : {player.GetDmg()} \t {monster.GetBoss()}의 공격력 : {monster.GetDmg()}");
            }
        }

        private void ShowState(Player.Player player)
        {
            WriteLine($"\n현재 턴 : {turn}");
            WriteLine($"{player.GetCharacter()}의 최대 체력 : {player.GetMaxHp()}");
            WriteLine($"{player.GetCharacter()}의 현재 체력 : {player.GetCurrentHp()}");
            WriteLine($"{player.GetCharacter()}의 공격력 : {player.GetDmg()}");
        }

        // --- Day 3 핵심: 외부 txt 파일 읽어오기 ---
        public void ShowPrologue(Chapter chapter)
        {
            // 경로 주의: 실제 프로젝트 폴더 안에 Scenario/Chapter1 폴더를 만들고 Prologue.txt를 넣어야 합니다.
            try
            {
                if (chapter == Chapter.CHAPTER1)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter1_Prologue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else if (chapter == Chapter.CHAPTER2)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter2_Prologue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter3_Prologue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine("스토리 파일을 찾을 수 없습니다: " + e.Message);
            }

            ConsoleClear();
        }

        public void ShowEpisode(Chapter chapter)
        {
            // 경로 주의: 실제 프로젝트 폴더 안에 Scenario/Chapter1 폴더를 만들고 Prologue.txt를 넣어야 합니다.
            try
            {
                if (chapter == Chapter.CHAPTER1)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter1_Episode.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else if (chapter == Chapter.CHAPTER2)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter2_Episode.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter3_Episode.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine("스토리 파일을 찾을 수 없습니다: " + e.Message);
            }

            if(chapter != Chapter.CHAPTER3) SetSkill(player);
            else Reinforce(player);

            WriteLine();
        }
        
        public void ShowEpilogue(Chapter chapter)
        {
            // 경로 주의: 실제 프로젝트 폴더 안에 Scenario/Chapter1 폴더를 만들고 Prologue.txt를 넣어야 합니다.
            try
            {
                if (chapter == Chapter.CHAPTER1)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter1_Epilogue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else if (chapter == Chapter.CHAPTER2)
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter2_Epilogue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(new FileStream($"..\\..\\..\\Scenario\\Chapter\\Chapter3_Epilogue.txt", FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            WriteLine(sr.ReadLine());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine("스토리 파일을 찾을 수 없습니다: " + e.Message);
            }

            ConsoleClear();
        }

        public void Reinforce(Player.Player player)
        {
            if(player.GetCharacter() == Character.VAMPIRE)
            {
                WriteLine("\n붉은 달이 끝까지 차오르자 그녀의 힘이 더욱 증폭되기 시작했다.");
                player.Enhance();
                WriteLine($"\n{player.GetCharacter()}의 최대 체력, 현재 체력, 공격력이 2배 증가했다.");
                ConsoleClear();
            }
        }

        private void FaceBoss(Chapter chapter)
        {
            int ranSkill = random.Next(0, 2);

            BossBattle(player, ref chapter, ref ranSkill);

            ConsoleClear();

            if (player.GetCurrentHp() <= 0)
            {
                if (chapter == Chapter.CHAPTER1)
                {
                    KilledByBoss(Monster.BOSS.PRIEST);
                    ConsoleClear();
                    Ending();
                }
                else if (chapter == Chapter.CHAPTER2)
                {
                    KilledByBoss(Monster.BOSS.VAN_HELSING);
                    ConsoleClear();
                    Ending();
                }
                else
                {
                    KilledByBoss(Monster.BOSS.WEREFENRIR);
                    ConsoleClear();
                    Ending();
                }
            }
        }

        // --- Day 3 핵심: 몬스터 랜덤 조우 로직 ---
        private void FaceMonster(Chapter chapter, ref int ranMob, ref int i)
        {
            if (chapter == Chapter.CHAPTER1)
            {
                Monster.Monster monster;

                if (ranMob > (int)Monster.MONSTER.WOLF) ranMob = (int)Monster.MONSTER.FARMER;
                turn = 1;

                if (ranMob == (int)Monster.MONSTER.FARMER)
                {
                    monster = new Monster.Farmer();
                    ConsoleClear(); // 직접 만든 메서드 사용!
                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else if (ranMob == (int)MONSTER.HUNTER)
                {
                    monster = new Hunter();
                    ConsoleClear();

                    Battle(player, monster);

                    // Battle이 끝난 후 플레이어가 죽었는지 판별
                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else
                {
                    monster = new Wolf();
                    ConsoleClear();

                    Battle(player, monster);

                    // Battle이 끝난 후 플레이어가 죽었는지 판별
                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                ConsoleClear();
            }
            else if (chapter == Chapter.CHAPTER2)
            {
                Monster.Monster monster;

                if (ranMob > (int)Monster.MONSTER.WEREWOLF) ranMob = (int)Monster.MONSTER.WOLF;
                turn = 1;

                if (ranMob == (int)MONSTER.WOLF)
                {
                    monster = new Monster.Wolf();
                    ConsoleClear(); // 직접 만든 메서드 사용!

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else if (ranMob == (int)MONSTER.KNIGHT)
                {
                    monster = new Knight();
                    ConsoleClear();

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else
                {
                    monster = new Werewolf();
                    ConsoleClear();

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                ConsoleClear();
            }
            else

            {
                Monster.Monster monster;

                if (ranMob > (int)Monster.MONSTER.LYCAN) ranMob = (int)Monster.MONSTER.WEREWOLF;
                turn = 1;

                if (ranMob == (int)MONSTER.WEREWOLF)
                {
                    monster = new Monster.Werewolf();
                    ConsoleClear(); // 직접 만든 메서드 사용!

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else if (ranMob == (int)MONSTER.PALADIN)
                {
                    monster = new Paladin();
                    ConsoleClear();

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                else
                {
                    monster = new Lycan();
                    ConsoleClear();

                    Battle(player, monster);

                    while (monster.GetCurrentHp() > 0)
                    {
                        // Battle이 끝난 후 플레이어가 죽었는지 판별
                        if (player.GetCurrentHp() <= 0)
                        {
                            KilledByMob(); // 플레이어가 몬스터에게 죽었을때
                            i = -1; // 다시 시작 시 stg가 1부터 다시 시작해야 되기 때문에
                            Ending();
                            return; // 루프를 빠져나가기 위한 return (기존 블로그는 break였으나 구조상 return이 안전)
                        }
                        Battle(player, monster);
                    }
                }
                ConsoleClear();
            }
            ranMob++;
        }

        private void KilledByMob()
        {
            using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\Monster\\KilledByMob.txt", FileMode.Open)))
            {
                while (!sr.EndOfStream)
                {
                    Write(sr.ReadToEnd());
                }
            }
        }

        private void Ending()
        {
            WriteLine("\n\n\t게임 오버\n");
            Write("[1] 다시 시작, [2] 종료 : ");
            string input = ReadLine();

            switch (input)
            {
                case "1":
                    WriteLine("\n\t게임을 다시 시작합니다.\n\n\n");
                    ConsoleClear();
                    chapter = Chapter.LOBBY;
                    Lobby();
                    break;
                case "2":
                    chapter = Chapter.OVER;
                    end = false;
                    WriteLine("\n\t게임을 종료합니다.\n");
                    break;
            }
        }

        private void OnPlayerDied(object dead)
        {
            chapter = Chapter.OVER;
        }

        private void Battle(Player.Player player, Monster.Monster monster)
        {
            while (player.GetCurrentHp() > 0)
            {
                ShowState(player, monster, turn);
                WriteLine("\n어떻게 공격할까?");
                player.ShowSkill();

                string input = ReadLine();
                bool bl = int.TryParse(input, out int selectedNum);
                if (selectedNum < 1 || selectedNum > player.GetSkillCount() || bl == false)
                {
                    continue;
                }

                player.Attack(player.GetSkill(selectedNum), monster);

                if (monster.GetCurrentHp() > 0)
                {
                    monster.Attack(player);
                    player.EndTurn(monster);
                    if (monster.GetCurrentHp() <= 0)
                    {
                        ConsoleClear();
                        monster.ShowDisappear(player, monster.GetMob());
                        player.ClearBattleEffects();
                        break;
                    }
                    WriteLine("\n\n");
                    ConsoleClear();
                }
                else // monster의 현재 체력이 0 이하면 실행
                {
                    ConsoleClear(); // 앞의 전투 메세지 클리어
                    monster.ShowDisappear(player, monster.GetMob());
                    player.ClearBattleEffects();
                    break;
                }

                // 플레이어 사망 시 상태 변경 및 루프 탈출
                if (player.GetCurrentHp() <= 0)
                {
                    chapter = Chapter.OVER;
                    break;
                }

                turn++;
            }
        }

        public void ConsoleClear()
        {
            WriteLine("다음으로 넘어가려면 아무키나 눌러주세요. ");
            next = ReadLine();
            if (next != null)
            {
                Clear();
            }
        }

        public bool End()
        {
            if (!end)
            {
                WriteLine("종료");
                WriteLine("계속하려면 아무 키나 누르십시오 . . .");
                ReadLine();
                end = false;
            }
            return end;
        }

        private void KilledByBoss(BOSS boss)
        {
            if (boss == BOSS.PRIEST)
            {
                using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\BOSS\\KilledByPRIEST.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream) { Write(sr.ReadToEnd()); }
                }
            }
            else if (boss == BOSS.VAN_HELSING)
            {
                using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\BOSS\\KilledByVan_Helsing.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream) { Write(sr.ReadToEnd()); }
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(new FileStream("..\\..\\..\\Scenario\\BOSS\\KilledByWereFenrir.txt", FileMode.Open)))
                {
                    while (!sr.EndOfStream) { Write(sr.ReadToEnd()); }
                }

            }
        }


        private void BossBattle(Player.Player player, ref Chapter chapter, ref int ranSkill)
        {
            Boss boss;
            if (chapter == Chapter.CHAPTER1)
            {
                boss = new Priest();
                ConsoleClear();

                while (player.GetCurrentHp() > 0 && boss.GetCurrentHp() > 0)
                {
                    ShowState(player, boss, turn);

                    if (player.IsParalyzed())
                    {
                        WriteLine("\n마비 상태라서 공격할 수 없다!");
                        player.SetParalyzed(-1);
                    }
                    else
                    {
                        WriteLine("\n어떻게 공격할까?");
                        player.ShowSkill();

                        string input = ReadLine();
                        bool bl = int.TryParse(input, out int selectedNum);
                        if (selectedNum < 1 || selectedNum > player.GetSkillCount() || bl == false) { continue; }

                        player.Attack(player.GetSkill(selectedNum), boss);
                    }

                    if (boss.GetCurrentHp() > 0)
                    {
                        boss.BossAttack(boss.GetSkill(ranSkill), ref player);
                        player.EndTurn(boss);
                        if (boss.GetCurrentHp() <= 0)
                        {
                            player.ClearBattleEffects();
                            break;
                        }
                        WriteLine("\n\n");
                        ConsoleClear();
                    }
                    else
                    {
                        player.ClearBattleEffects();
                        break;
                    }

                    ranSkill++;
                    if (ranSkill > (int)Monster.PriestSkill.PRAY)
                    {
                        ranSkill = (int)Monster.PriestSkill.READSPELL;
                    }

                    turn++;
                }
            }
            else if (chapter == Chapter.CHAPTER2)
            {
                boss = new Van_Helsing();
                ConsoleClear();

                while (player.GetCurrentHp() > 0 && boss.GetCurrentHp() > 0)
                {
                    ShowState(player, boss, turn);

                    if (player.IsParalyzed())
                    {
                        WriteLine("\n마비 상태라서 공격할 수 없다!");
                        player.SetParalyzed(-1);
                    }
                    else
                    {
                        WriteLine("\n어떻게 공격할까?");
                        player.ShowSkill();

                        string input = ReadLine();
                        bool bl = int.TryParse(input, out int selectedNum);
                        if (selectedNum < 1 || selectedNum > player.GetSkillCount() || bl == false) { continue; }

                        player.Attack(player.GetSkill(selectedNum), boss);
                    }

                    if (boss.GetCurrentHp() > 0)
                    {
                        boss.BossAttack(boss.GetSkill(ranSkill), ref player);
                        player.EndTurn(boss);
                        if (boss.GetCurrentHp() <= 0)
                        {
                            player.ClearBattleEffects();
                            break;
                        }
                        WriteLine("\n\n");
                        ConsoleClear();
                    }
                    else
                    {
                        player.ClearBattleEffects();
                        break;
                    }

                    ranSkill++;
                    if (ranSkill > (int)Monster.Van_HelsingSkill.POISONARROW)
                    {
                        ranSkill = (int)Monster.Van_HelsingSkill.SILVERARROW;
                    }

                    turn++;
                }
            }
            else
            {
                boss = new WereFenrir();
                ConsoleClear();

                while (player.GetCurrentHp() > 0 && boss.GetCurrentHp() > 0)
                {
                    ShowState(player, boss, turn);

                    if (player.IsParalyzed())
                    {
                        WriteLine("\n마비 상태라서 공격할 수 없다!");
                        player.SetParalyzed(-1);
                    }
                    else
                    {
                        WriteLine("\n어떻게 공격할까?");
                        player.ShowSkill();

                        string input = ReadLine();
                        bool bl = int.TryParse(input, out int selectedNum);
                        if (selectedNum < 1 || selectedNum > player.GetSkillCount() || bl == false) { continue; }

                        player.Attack(player.GetSkill(selectedNum), boss);
                    }

                    if (boss.GetCurrentHp() > 0)
                    {
                        boss.BossAttack(boss.GetSkill(ranSkill), ref player);
                        player.EndTurn(boss);
                        if (boss.GetCurrentHp() <= 0)
                        {
                            player.ClearBattleEffects();
                            break;
                        }
                        WriteLine("\n\n");
                        ConsoleClear();
                    }
                    else
                    {
                        player.ClearBattleEffects();
                        break;
                    }

                    ranSkill++;
                    if (ranSkill > (int)Monster.WereFenrirSkill.BITE)
                    {
                        ranSkill = (int)Monster.WereFenrirSkill.MOONHOPER;
                    }

                    turn++;
                }
            }
        }

        private void SetSkill(Player.Player player)
        {
            if (player.GetCharacter() == Character.VAMPIRE)
            {
                while (true)
                {
                    int randomNum = random.Next((int)VampireSkill.Bat_Fire, (int)VampireSkill.Blood_Diamond + 1);
                    int skill = randomNum;
                    if (player.CheckSkill(skill)) continue;
                    else
                    {
                        player.SetSkill(skill);
                        WriteLine($"\n{(VampireSkill)skill}이(가) 추가되었습니다.\n");
                        break;
                    }
                }
            }

            ConsoleClear();
        }

        private void Initialization(Player.Player player)
        {
            player.GetHeal(player.GetMaxHp()); // 체력 최대로 회복
            Clear();
            ShowState(player);

            WriteLine("\n달의 가호로 모든 체력을 회복했다.\n");

            ConsoleClear();
        }
    }
}
