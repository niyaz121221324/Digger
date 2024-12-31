using Digger.Architecture;
using System;

namespace Digger.GameObjects
{
    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var playerPosition = GetPlayerPosition();

            if (!playerPosition.HasValue)
            {
                return new CreatureCommand();
            }

            var command = GetMovement(playerPosition.Value, (x, y));

            return command;
        }

        private (int pX, int pY)? GetPlayerPosition()
        {
            for (int x = 0; x < Game.MapWidth; x++)
            {
                for (int y = 0; y < Game.MapHeight; y++)
                {
                    if (Game.Map[x, y] is Player)
                    {
                        return (x, y);
                    }
                }
            }

            return null;
        }


        private CreatureCommand GetMovement((int pX, int pY)? playerPosition, (int x, int y) enemyPosition)
        {
            CreatureCommand command = new CreatureCommand();

            if (!playerPosition.HasValue)
            {
                return command;
            }

            int deltaX = Math.Sign(playerPosition.Value.pX - enemyPosition.x);

            // Определяем движение по оси X
            if (deltaX != 0)
            {
                SetEnemyMovementX(deltaX, enemyPosition, command);
            }

            int deltaY = Math.Sign(playerPosition.Value.pY - enemyPosition.y);

            // Определяем движении по оси Y
            if (deltaY != 0)
            {
                SetEnemyMovementY(deltaY, enemyPosition, command);
            }

            return command;
        }

        private void SetEnemyMovementX(int deltaX, (int x, int y) enemyPosition, CreatureCommand command)
        {
            if (deltaX < 0 && CanMoveTo(enemyPosition.x, enemyPosition.y, Direction.Left))
            {
                command.DeltaX--;
            }
            else if (deltaX > 0 && CanMoveTo(enemyPosition.x, enemyPosition.y, Direction.Right))
            {
                command.DeltaX++;
            }
        }

        private void SetEnemyMovementY(int deltaY, (int x, int y) enemyPosition, CreatureCommand command)
        {
            if (deltaY < 0 && CanMoveTo(enemyPosition.x, enemyPosition.y, Direction.Up))
            {
                command.DeltaY--;
            }
            else if (deltaY > 0 && CanMoveTo(enemyPosition.x, enemyPosition.y, Direction.Down))
            {
                command.DeltaY++;
            }
        }

        private bool CanMoveTo(int x, int y, Direction direction)
        {
            return direction switch
            {
                Direction.Left when x > 0 => IsSafeObject(Game.Map[x - 1, y]),
                Direction.Right when x < Game.MapWidth - 1 => IsSafeObject(Game.Map[x + 1, y]),
                Direction.Up when y > 0 => IsSafeObject(Game.Map[x, y - 1]),
                Direction.Down when y < Game.MapHeight - 1 => IsSafeObject(Game.Map[x, y + 1]),
                _ => false
            };
        }

        private bool IsSafeObject(ICreature gameObject)
        {
            return gameObject == null || gameObject is Player || gameObject is Gold;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
}
