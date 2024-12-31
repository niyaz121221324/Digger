using Avalonia.Input;
using Digger.Architecture;

namespace Digger.GameObjects
{
    class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var movement = GetMovement(x, y);

            return new CreatureCommand()
            {
                DeltaX = movement.deltaX,
                DeltaY = movement.deltaY,
                TransformTo = new Player()
            };
        }

        private (int deltaX, int deltaY) GetMovement(int x, int y)
        {
            return Game.KeyPressed switch
            {
                Key.Left when x > 0 && Game.Map[x - 1, y] is not Sack => (-1, 0),
                Key.Right when x < Game.MapWidth - 1 && Game.Map[x + 1, y] is not Sack => (1, 0),
                Key.Up when y > 0 && Game.Map[x, y - 1] is not Sack => (0, -1),
                Key.Down when y < Game.MapHeight - 1 && Game.Map[x, y + 1] is not Sack => (0, 1),
                _ => (0, 0)
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }
}
