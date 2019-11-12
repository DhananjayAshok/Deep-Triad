using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMatrix
{
    public int[,,] gameMatrix = new int[3, 3, 3]; // Public only for now
   private ArrayList winLines;
    private Stack<int> moveStack;

    public GameMatrix() {
        moveStack = new Stack<int>();
        // Create and set all the winLines. From the notepad files
        winLines = new ArrayList();

        winLines.Add(new Line(new int[] {0,0,0 }, new int[] {0,0,1}));
        winLines.Add(new Line(new int[] { 0, 1, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 0, 2, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 1 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 0, 1, 1 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 0, 1, -1 }));

        winLines.Add(new Line(new int[] { 1, 0, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 1, 1, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 1, 2, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 1, 0, 0 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 1, 0, 1 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 1, 0, 2 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 1, 0, 0 }, new int[] { 0, 1, 1 }));
        winLines.Add(new Line(new int[] { 1, 0, 2 }, new int[] { 0, 1, -1 }));

        winLines.Add(new Line(new int[] { 2, 0, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 2, 1, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 2, 2, 0 }, new int[] { 0, 0, 1 }));
        winLines.Add(new Line(new int[] { 2, 0, 0 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 2, 0, 1 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 2, 0, 2 }, new int[] { 0, 1, 0 }));
        winLines.Add(new Line(new int[] { 2, 0, 0 }, new int[] { 0, 1, 1 }));
        winLines.Add(new Line(new int[] { 2, 0, 2 }, new int[] { 0, 1, -1 }));

        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 1, 0 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 0 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 1 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 1, 1 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 1, 2 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 1 }, new int[] { 1, 0, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 2 }, new int[] { 1, 0, 0 }));

        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 1, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 0 }, new int[] { 1, -1, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 1, 0, 1 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 1, 0, -1 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 1, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 2 }, new int[] { 1, -1, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 0 }, new int[] { 1, 0, 1 }));
        winLines.Add(new Line(new int[] { 0, 2, 2 }, new int[] { 1, 0, -1 }));

        winLines.Add(new Line(new int[] { 0, 0, 0 }, new int[] { 1, 1, 1 }));
        winLines.Add(new Line(new int[] { 0, 0, 1 }, new int[] { 1, 1, 0 }));
        winLines.Add(new Line(new int[] { 0, 0, 2 }, new int[] { 1, 1, -1 }));
        winLines.Add(new Line(new int[] { 0, 1, 0 }, new int[] { 1, 0, 1 }));
        winLines.Add(new Line(new int[] { 0, 1, 2 }, new int[] { 1, 0, -1 }));
        winLines.Add(new Line(new int[] { 0, 2, 0 }, new int[] { 1, -1, 1 }));
        winLines.Add(new Line(new int[] { 0, 2, 1 }, new int[] { 1, -1, 0 }));
        winLines.Add(new Line(new int[] { 0, 2, 2 }, new int[] { 1, -1, -1 }));


    }

    public int[] DecodeMove(int move) {
        int col = (move - 1) % 3; // Without -1 -> 1, 4, 7 would be 1| 2, 5, 8 would be 2| 3, 6, 9 would be 0
        int row;
        int layer = 3;

        if (move > 6)
        {
            row = 2;
        }
        else if (move > 3)
        {
            row = 1;
        }
        else
        {
            row = 0;
        }
        if (gameMatrix[0, row, col] == 0)
        {
            layer = 0;
        }
        else if (gameMatrix[1, row, col] == 0)
        {
            layer = 1;
        }
        else if (gameMatrix[2, row, col] == 0)
        {
            layer = 2;
        }

        return new int[] { layer, row, col };
    } // Public only for now
    public int[] DecodeMove(int move, int[,,] clone){
        int col = (move - 1) % 3; // Without -1 -> 1, 4, 7 would be 1| 2, 5, 8 would be 2| 3, 6, 9 would be 0
        int row;
        int layer = 3;

        if (move > 6)
        {
            row = 2;
        }
        else if (move > 3)
        {
            row = 1;
        }
        else
        {
            row = 0;
        }
        if (clone[0, row, col] == 0)
        {
            layer = 0;
        }
        else if (clone[1, row, col] == 0)
        {
            layer = 1;
        }
        else if (clone[2, row, col] == 0)
        {
            layer = 2;
        }

        return new int[] { layer, row, col };
    }

    public void RegisterMove(int move, int playerID)
    {
        moveStack.Push(move);
        int[] decoded = DecodeMove(move);
        gameMatrix[decoded[0], decoded[1], decoded[2]] = playerID;
    }

    public void SimulateMove(int move, int playerID, int[,,] clone) {
        int[] decoded = DecodeMove(move, clone);
        clone[decoded[0], decoded[1], decoded[2]] = playerID;
    }

    public bool MoveLegal(int move)
    {
        if (move < 1 || move > 9)
        {
            return false;
        }
        int[] decoded = DecodeMove(move);
        return decoded[0] <= 2;

    }

    public int DeregisterMove()
    {
        int move = moveStack.Pop();
        int[] decoded = DecodeMove(move);
        if (gameMatrix[2, decoded[1], decoded[2]] != 0)
        {
            gameMatrix[2, decoded[1], decoded[2]] = 0;
        }
        else if (gameMatrix[1, decoded[1], decoded[2]] != 0)
        {
            gameMatrix[1, decoded[1], decoded[2]] = 0;
        }
        else
        {
            gameMatrix[0, decoded[1], decoded[2]] = 0;
        }
        return move;
    }

    public int PieceAt(int[] pos) {
        int layer = pos[0];
        int row = pos[1];
        int column = pos[2];
        //Debug.Log(pos[0] + "  " + pos[1] + "  " + pos[2]);

        return gameMatrix[layer, row, column];
    }

    public int PieceAt(int[] pos, int[,,] clone) {
        int layer = pos[0];
        int row = pos[1];
        int column = pos[2];
        //Debug.Log(pos[0] + "  " + pos[1] + "  " + pos[2]);

        return clone[layer, row, column];
    }

    public bool noMoves() {
        return (moveStack.Count == 0);
    }

    public int CheckForWin() {
        foreach (Line line in winLines){
            if (PieceAt(line.point0) == PieceAt(line.point1) && PieceAt(line.point1) == PieceAt(line.point2)) {
                if (PieceAt(line.point0) != 0) {
                    return PieceAt(line.point0);
                }
            }
        }
        return 0;
    }

    public int CheckForWin(int[,,] clone) {
        foreach (Line line in winLines)
        {
            if (PieceAt(line.point0, clone) == PieceAt(line.point1, clone) && PieceAt(line.point1, clone) == PieceAt(line.point2, clone))
            {
                if (PieceAt(line.point0, clone) != 0)
                {
                    return PieceAt(line.point0, clone);
                }
            }
        }
        return 0;
    }

    public int[,,] cloneGame() {
        // Need to find a way to de-alias it
        return (int[,,])gameMatrix.Clone();
    }

    public int calculateAttackScore(int move, int playerID)
    {
        int[,,] clone = cloneGame();
        SimulateMove(move, playerID, clone);

        int score = 1;
        foreach (Line line in winLines) {
            int miniscore = 1;
            if (PieceAt(line.point0) != 0 && PieceAt(line.point0) != playerID)
            {
                
            }
            else if (PieceAt(line.point1) != 0 && PieceAt(line.point1) != playerID)
            {

            }
            else if (PieceAt(line.point2) != 0 && PieceAt(line.point2) != playerID)
            {

            }
            else {
                if (PieceAt(line.point0, clone) == playerID)
                {
                    if (PieceAt(line.point1, clone) == playerID)
                    {
                        if (PieceAt(line.point2, clone) == playerID)
                        {
                            miniscore += 1000;
                            // 3
                        }
                        else
                        {
                            miniscore += 4;
                            // 2
                        }
                    }
                    else if (PieceAt(line.point2, clone) == playerID)
                    {
                        miniscore += 4;
                        // 2
                    }
                    else
                    {
                        miniscore += 1;
                        // 1
                    }
                }
                else if (PieceAt(line.point1, clone) == playerID)
                {
                    if (PieceAt(line.point2, clone) == playerID)
                    {
                        miniscore += 4;
                        // 2
                    }
                    else
                    {
                        miniscore += 1;
                        // 1
                    }
                }
                else if (PieceAt(line.point2, clone) == playerID)
                {
                    miniscore += 1;
                    // 1
                }
                else
                {
                    // 0
                }
            }
             
            score *= miniscore; // Could do addition
        }
        return score;
    }
    public int MoveWinner(int move, int playerID) {
        // Returns the winner value after a specific move is played.
        int[,,] clone = cloneGame();
        SimulateMove(move, playerID, clone);
        return CheckForWin(clone);
    }

}
