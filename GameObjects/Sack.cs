using Digger.Architecture;

namespace Digger.GameObjects
{
    class Sack : ICreature
    {
        private int stepsToDown;

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand command = new CreatureCommand();

            if (IsFallingDown(x, y))
            {
                command.DeltaY++;

                stepsToDown++;
            }

            if (IsHit(x, y))
            {
                command.TransformTo = stepsToDown > 1 ? new Gold() : new Sack();
            }

            return command;
        }

        private bool IsFallingDown(int x, int y)
        {
            if (y >= Game.MapHeight - 1)
            {
                return false;
            }

            bool isEmptyCell = Game.Map[x, y + 1] == null;
            bool isSackFallingOnEntity = (Game.Map[x, y + 1] is Monster || Game.Map[x, y + 1] is Player) && stepsToDown > 0;

            return isEmptyCell || isSackFallingOnEntity;
        }

        private bool IsHit(int x, int y)
        {
            if (y + 1 == Game.MapHeight)
            {
                return true;
            }

            return Game.Map[x, y + 1] != null && 
                   Game.Map[x, y + 1] is not Player && 
                   Game.Map[x, y + 1] is not Monster;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
