using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public SpawnManager sm;
    public int winner = 0;

    private Stack<int> moveStack;
    private int turn = 0;
    // Representation of the game pieces. 
    /*
     * gameMatrix[layer,row, column]
     * gameMatrix[0, 0] is the 1st row of the bottom layer
     * if gameMatrix[0,0,0] = x then if x = 0 its empty else depending on 1,2,3 its filled.
     */
    public int[, , ] gameMatrix = new int[3,3,3];
    // 0 is user, 1 through 3 is Triad.
    public int player1 = 0;
    public int player2 = 0;
    public int player3 = 0;
    private int[] players;
    private WinFinder wf;
    private WinManager wm;
    private AIPlaceManager ai;

    
    
    // Start is called before the first frame update
    void Start()
    {
        moveStack = new Stack<int>();
        winner = 0;
        wf = GetComponent<WinFinder>();
        wm = GetComponent<WinManager>();
        ai = GetComponent<AIPlaceManager>();
        players = new int[] { player1, player2, player3 };
    }

    int Listener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 9;
        }
        else
        {
            return 0;
        }

    }        
    
    public int[] DecodeMove(int move) {
        int col = (move-1) % 3; // Without -1 -> 1, 4, 7 would be 1| 2, 5, 8 would be 2| 3, 6, 9 would be 0
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
        else if (gameMatrix[1, row, col] == 0) {
            layer = 1;
        }
        else if (gameMatrix[2, row, col] == 0)
        {
            layer = 2;
        }

        return new int[] { layer, row, col };

    }
    bool canPlay() { 
        return (winner == 0);
    }
    bool isHumanTurn(int playerID)
    {
        return (players[playerID-1] == 0);
    }
    bool moveLegal(int move) {
        if (move < 1 || move > 9) {
            return false;
        }
        int[] decoded = DecodeMove(move);
        return decoded[0] <= 2;
    }

    void RegisterMove(int move, int playerID) {
        moveStack.Push(move);
        int[] decoded = DecodeMove(move);
        gameMatrix[decoded[0], decoded[1], decoded[2]] = playerID;
    }
    int DeregisterMove() {
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
        else {
            gameMatrix[0, decoded[1], decoded[2]] = 0;
        }
        return move;
    }

    // Returns true iff a succesful move was executed
    int HumanMove() {
        int move = Listener();
        return move;
    }

    int AIMove() {

        int move = ai.RandomMove();

        return move;
    }

    void MakeMove() {
        int playerID = (turn % 3) + 1;
        if (sm.CanPlay())
        {
            int move = 0;
            if (isHumanTurn(playerID))
            {
                move = HumanMove();
            }
            else
            {
                //Debug.Log("AI check was entered");
                if (Input.GetKeyDown(KeyCode.Return)) {
                    //Debug.Log("Enter was hit");
                    move = AIMove(); 
                
                }
            }
            if (moveLegal(move)) {
                sm.SpawnSequence(playerID, move);
                RegisterMove(move, playerID);
                winner = wf.CheckForWinner(move);
                turn++;
                int newPlayerID = (turn % 3) + 1;
                wm.TurnChange(newPlayerID);
                if (winner != 0) {
                    wm.WinSequence(winner);
                }

            }
        }
    }
    void UnmakeMove() {
        if (sm.CanPlay(false))
        {
            turn--;
            wm.TurnChange((turn % 3) + 1);
            if (winner != 0) {
                wm.UndoWin();
            }
            winner = 0;
            int move = DeregisterMove();
            sm.DeSpawnSequence(move);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (canPlay()) {
            MakeMove();
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && moveStack.Count!=0) {
            UnmakeMove();
        }
    }
}
