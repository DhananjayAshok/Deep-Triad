using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public int RandomMove(int playerID, GameMatrix gm) {
        int pWinCheck = PrimaryWinMove(playerID, gm);
        if (pWinCheck != 0) { return pWinCheck; }

        int pBlockCheck = PrimaryBlockMove(playerID, gm);
        if (pBlockCheck != 0){return pBlockCheck;}


        int value = (int)(Random.value * 10);
        value = value % 9;
        value = value + 1;
        return value;
    }

    public int AggresiveMove(int playerID ,GameMatrix gm) {
        int pWinCheck = PrimaryWinMove(playerID, gm);
        if (pWinCheck != 0) { return pWinCheck; }
        
        int pBlockCheck = PrimaryBlockMove(playerID, gm);
        if (pBlockCheck != 0) {
            return pBlockCheck;
        }
        
        int best = 0;
        int bestmove = 0;
        for (int iii = 1; iii < 10; iii++) {
            if (gm.MoveLegal(iii))
            {
                int value = gm.calculateAttackScore(iii, playerID);
                if (value >= best)
                {
                    bestmove = iii;
                    best = value;
                }
            } 
        }
        return bestmove;
    }

    private int PrimaryBlockMove(int playerID, GameMatrix gm)
    {
        int nextPlayer;
        if (playerID == 3)
        {
            nextPlayer = 1;
        }
        else
        {
            nextPlayer = playerID + 1;
        }
        for (int iii = 1; iii < 10; iii++)
        {
            if (gm.MoveLegal(iii))
            {
                if (gm.MoveWinner(iii, nextPlayer) == nextPlayer)
                {
                    return iii;
                }
            }
        }
        return 0;
    }

    private int PrimaryWinMove(int playerID, GameMatrix gm) {
        for (int iii = 1; iii < 10; iii++)
        {
            if (gm.MoveLegal(iii))
            {
                if (gm.MoveWinner(iii, playerID) == playerID)
                {
                    return iii;
                }
            }
        }
        return 0;
    }


    public int HandleAIMove(int playerID, GameMatrix gm, int AIinfo) {
        if (AIinfo == 1)
        {
            return AggresiveMove(playerID, gm);
        }
        else {
            return RandomMove(playerID, gm);
        }
    
    }

    void Update()
    {
        
    }
}
