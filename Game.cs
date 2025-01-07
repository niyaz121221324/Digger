using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public static class Game
{
    private const string mapString = @"
PTTGTT TST
TST  TSTTM
TTT TTSTTT
T TSTS TTT
T TTTGMSTS
T TMT M TS
TSTSTTMTTT
S TTST  GG
 TGST MTTT
 T  TMTTTT";

    public static ICreature[,] Map;
    public static int Scores;
    public static bool IsOver;

    public static Key KeyPressed;
    public static int MapWidth => Map.GetLength(0);
    public static int MapHeight => Map.GetLength(1);

    public static void CreateMap()
    {
        Map = CreatureMapCreator.CreateMap(mapString);
    }
}