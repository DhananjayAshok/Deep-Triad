using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WinFinder : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;

    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    int[] addition(int[] vector, int[] vector1) {
        return new int[] { vector[0] + vector1[0], vector[1] + vector1[1], vector[2] + vector1[2]};        
    }

    int[] invert(int[] vector) {
        return new int[] { -1 * vector[0], -1 * vector[1], -1 * vector[2] };
    }
    bool VectorInRange(int[] vector) {
        foreach (int n in vector) {
            if (n < 0 || n > 2)
            {
                return false;
            }
        }
        return true;
        
    }
    
    
    // Returns the directions that are both valid directions to move in and also that have the same playerID.
    ArrayList Scan(int[] decoded_move, int playerID)
    {
        ArrayList vectors = new ArrayList();
        ArrayList legalVectors = new ArrayList();
        ArrayList matchingVectors = new ArrayList();
        // Generate an arraylist with 27 vectors each of which are just one away from the current spot.
        for(int n=-1; n <=1; n++) {
            for(int n1=-1; n1<=1; n1++) {
                for(int n2=-1; n2<=1; n2++) {
                    if ((n == 0) && (n1 == 0) && (n2 == 0))
                    {

                    }
                    else {
                        vectors.Add(new int[] { n, n1, n2 });
                    }
                }
            }
        }
        //Debug.Log("Directions at first are "+vectors.Count);
        foreach (int[] v in vectors) {
            if (VectorInRange(addition(decoded_move, v))) {
                legalVectors.Add(v);
            }
        }
        //Debug.Log("Directions after range check is " + legalVectors.Count);
        foreach (int[] v in legalVectors) {
            int[] vector = addition(decoded_move, v);
            if (gm.gameMatrix[vector[0], vector[1], vector[2]] == playerID) {
                matchingVectors.Add(v);
            }
        }
        //Debug.Log("Finally " + matchingVectors.Count);
        return matchingVectors;

    }

    bool FindWins(ArrayList scanned, int[] decoded_move, int playerID) {
        foreach (int[] direction in scanned) {
            // If you are the end of the chain of 3
            int[] first = decoded_move;
            int[] middle = addition(decoded_move, direction);
            int[] last = addition(middle, direction);
            if (VectorInRange(last))
            {
                if (gm.gameMatrix[last[0], last[1], last[2]] == playerID) {return true;}
            }
            // If you are in the middle
            middle = decoded_move;
            first = addition(middle, invert(direction));
            last = addition(middle, direction);
            if (VectorInRange(first)) {
                if (gm.gameMatrix[first[0], first[1], first[2]] == playerID) { return true; }
            }
        }
        return false;
    }


    public int CheckForWinner(int move) {
        int[] decoded = gm.DecodeMove(move); // Layer will be wrong. i.e it will be one more than actual
        decoded[0] = decoded[0] - 1;
        int layer = decoded[0];
        int row = decoded[1];
        int column = decoded[2];
        int playerID = gm.gameMatrix[layer, row, column];
        ArrayList scanned = Scan(decoded, playerID);
        if (FindWins(scanned, decoded, playerID)){
            return playerID;
        }
        else
        {
            return 0;

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
