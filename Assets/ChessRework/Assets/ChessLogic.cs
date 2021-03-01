using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChessLogic
{
    public static Vector2Int North = new Vector2Int( 0, 1);
    public static Vector2Int NorthEast = new Vector2Int( 1, 1);
    public static Vector2Int East = new Vector2Int( 1, 0);
    public static Vector2Int SouthEast = new Vector2Int( 1, -1);
    public static Vector2Int South = new Vector2Int( 0, -1);
    public static Vector2Int SouthWest = new Vector2Int(-1, -1);
    public static Vector2Int West = new Vector2Int( -1, 0);
    public static Vector2Int NorthWest = new Vector2Int( -1, 1);

    //Get All legal Moves North, East, South, West, with a given distance from start.
    public static List<Vector2Int> GetAdjacent(Board board, Vector2Int startIndex, int distance)
    {

        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        Vector2Int index = startIndex;

        AllowedMoves.AddRange(CheckDirection(board, index, North, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, East, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, South, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, West, distance));

        return AllowedMoves;
    }

    //Get All legal Moves on Diagonals with a given distance from start.
    public static List<Vector2Int> GetDiagonals(Board board, Vector2Int startIndex, int distance)
    {

        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        Vector2Int index = startIndex;

        AllowedMoves.AddRange(CheckDirection(board, index, NorthEast, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, SouthEast, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, SouthWest, distance));
        AllowedMoves.AddRange(CheckDirection(board, index, NorthWest, distance));

        return AllowedMoves;
    }

    //Get All legal Moves on Diagonals with a given distance from start.
    public static List<Vector2Int> CheckSpots(Board board, Vector2Int startIndex, List<Vector2Int> spots)
    {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();

        foreach (Vector2Int spot in spots) {
            if (CheckSpot(board, spot, board.boardState[startIndex.x, startIndex.y].chessPiece.team))
            {
                AllowedMoves.Add(spot);
            }
        }

        return AllowedMoves;
    }

    public static bool CheckSpot(Board board, Vector2Int customSpot, Team myTeam) {
        
        if (!board.IsInBounds(customSpot))
        {
            return false;
        }
        if (board.boardState[customSpot.x, customSpot.y].isBlocked)
        {
            return false;
        }
        if (board.boardState[customSpot.x, customSpot.y].chessPiece != null)
        {
            if (board.boardState[customSpot.x, customSpot.y].chessPiece.team == myTeam)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return true;
        }
    }

    //Get All legal Moves with a given direction with a given distance from start.
    public static List<Vector2Int> CheckDirection( Board board, Vector2Int index, Vector2Int direction, int distance)
    {

        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        Team myTeam = board.boardState[index.x, index.y].chessPiece.team;

        //Go in a given direction until we hit a edge of board, blockage, or allied peice
        for (int i = 0; i < distance; i ++) {
            index += direction;
            
            if (!board.IsInBounds(index)){
                break;
            }
            if (board.boardState[index.x, index.y].isBlocked)
            {
                break;
            }
            if (board.boardState[index.x, index.y].chessPiece != null)
            {
                if (board.boardState[index.x, index.y].chessPiece.team == myTeam)
                {
                    break;
                }
                else
                {
                    AllowedMoves.Add(index);
                    break;
                }

            }

            AllowedMoves.Add(index);
        }


        return AllowedMoves;
    }

}

public class Square
{

    public ChessPiece chessPiece;
    public Obstacle obstacle;
    public SpellEffect spellEffect;
    bool validTerrain;
    public bool isBlocked
    {
        get
        {
            if (obstacle != null) return false;
            else if (!validTerrain) return false;
            else if (spellEffect != null)
            {
                if (spellEffect.ActAsObstacle())
                    return false;
                else return true;
            }
            else return true;
        }

    }

    public Square(bool isTerrainValid)
    {
        validTerrain = isTerrainValid;
    }

}
