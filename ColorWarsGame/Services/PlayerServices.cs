using ColorWars.Boards;
using ColorWars.Players;
using System.Collections.Generic;
using System.Linq;

namespace ColorWars.Services
{
    class PlayerServices
    {
        public static void AddTerritory(PlayerModel player)
        {
            List<BoardField> tailNeighbours = new List<BoardField>();
            List<BoardField> outerNeighbours = new List<BoardField>();

            foreach(BoardField field in player.Tail.Positions)
            {
                // Conquer all fields from tail
                field.Owner = player;

                // Create list of all not taken neighbours of every tail field
                foreach(BoardField neighbour in field.Neighbours.Values)
                {
                    if(neighbour != null && neighbour.Owner != player && !tailNeighbours.Contains(neighbour))
                    {
                        tailNeighbours.Add(neighbour);
                    }
                }
            }
            foreach(BoardField startField in tailNeighbours)
            {
                // check for performance boost (won't start algorithm for fields already classified as outer or taken,
                // so most of the cases algorithm will run only twice)
                if(startField.Owner != player && !outerNeighbours.Contains(startField))
                {
                    // this is one area of adjecent, not taken fields
                    List<BoardField> joinedArea = new List<BoardField>();
                    // algorithm returns bool value indicating if any of the fields in joined area don't have null neighbour
                    bool joinedAreaIsInside = ClaimTerritoryAlgorithm(player, startField, outerNeighbours, joinedArea);
                    if(joinedAreaIsInside)
                    {
                        // if so, joined area is inside players' terrain
                        foreach(BoardField field in joinedArea)
                        {
                            field.Owner = player;
                        }
                    }
                }
            }
        }

        private static bool ClaimTerritoryAlgorithm(PlayerModel player, BoardField currentField, List<BoardField> outerNeighbours, List<BoardField> joinedArea)
        {
            joinedArea.Add(currentField);
            if(currentField.Neighbours.Values.Any(f => f == null))
            {
                outerNeighbours.AddRange(joinedArea.ToArray());
                return false;
            }
            //For each of neighbours thatarent in joined area list and does not belong to player make recurrent call
            bool EachNeighbourReturnsTrue = true;
            foreach(BoardField neighbour in currentField.Neighbours.Values)
            {
                if(neighbour.Owner != player && !joinedArea.Contains(neighbour))
                {
                    if(!ClaimTerritoryAlgorithm(player, neighbour, outerNeighbours, joinedArea))
                    {
                        EachNeighbourReturnsTrue = false;
                    }
                }
            }
            return EachNeighbourReturnsTrue;
        }

        public static Direction ReversedDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.UP:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.UP;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.LEFT;
                default:
                    return Direction.NONE;
            }
        }
    }
}
