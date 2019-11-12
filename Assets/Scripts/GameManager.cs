using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public SpawnManager sm;
    public int winner = 0;

    private int turn = 0;
    public GameMatrix gm;
    // 0 is user, 1 through 3 is Triad.
    public int player1 = 0;
    public int player2 = 0;
    public int player3 = 0; // Current plan is 2 distinct AI so 1 and 2.
    private int[] players;
    private WinManager wm;
    private AIPlaceManager ai;

    
    
    // Start is called before the first frame update
    void Start()
    {
        winner = 0;
        wm = GetComponent<WinManager>();
        ai = GetComponent<AIPlaceManager>();
        setPlayers(MenuManager.AIPlayers);
        players = shuffle(player1, player2, player3);
        gm = new GameMatrix();
    }

    void setPlayers(int no_AI)
    {
        if (no_AI == 0)
        {
            player1 = 0;
            player2 = 0;
            player3 = 0;
        }
        else if (no_AI == 1)
        {
            player1 = 0;
            player2 = 0;
            float r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player3 = 1; } else { player3 = 2; }

        }
        else if (no_AI == 2) {
            player1 = 0;
            float r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player2 = 1; } else { player2 = 2; }
            r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player3 = 1; } else { player3 = 2; }
        }
        else
        {
            float r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player1 = 1; } else { player1 = 2; }
            r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player2 = 1; } else { player2 = 2; }
            r = Random.Range(0.0f, 1.0f);
            if (r < 0.5) { player3 = 1; } else { player3 = 2; }

        }

    }

    int[] shuffle(int player1, int player2, int player3) {
        int[] final = new int[3];
        bool[] assigned = new bool[] { false, false, false };
        int r = ((int)(Random.value*10))%3; // Turn of player1
        final[r] = player1; // The rth entry of final is player1
        assigned[r] = true;
        //Debug.Log(("At first r was ", r, " so final[r] is ", final[r]));
        r = ((int)(Random.value * 10)) % 3; // To be turn of player2
        while (assigned[r]) { // When r is already assigned.
            r = ((int)(Random.value * 10)) % 3; // Change r and look for an alternative.
        }
        final[r] = player2;
        assigned[r] = true;
        //Debug.Log(("After looking 2nd time r was ", r, " so final[r] is ", final[r]));
        for (int i = 0; i < assigned.Length; i++) {
            if (!assigned[i]) {
                final[i] = player3;
                assigned[i] = true;
                //Debug.Log(("Finally r was ", i, " so final[r] is ", final[i]));
                return final;
            }
        }
        return final;



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
   
    bool canPlay() { 
        return (winner == 0);
    }
    bool isHumanTurn(int playerID)
    {
        //Debug.Log(("id ", playerID, " and player is ", players[playerID-1]));
        return (players[playerID-1] == 0);
    }
    
    // Returns true iff a succesful move was executed
    int HumanMove() {
        int move = Listener();
        return move;
    }

    int AIMove(int playerID) {
        
        int move = ai.HandleAIMove(playerID, gm, players[playerID-1]);

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
                    move = AIMove(playerID); 
                
                }
            }
            if (gm.MoveLegal(move)) {
                sm.SpawnSequence(playerID, move);
                //Debug.Log(gm.calculateAttackScore(move, playerID));
                gm.RegisterMove(move, playerID);
                winner = gm.CheckForWin();
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
            int move = gm.DeregisterMove();
            sm.DeSpawnSequence(move);
        }

    }
    // Update is called once per frame

    public void ExitToMenu() {
        SceneManager.LoadSceneAsync("Main Menu");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { ExitToMenu(); return;}

        if (canPlay()) {
            MakeMove();
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && !gm.noMoves()) {
            UnmakeMove();
        }
    }
}
