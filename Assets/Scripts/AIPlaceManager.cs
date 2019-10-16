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

    public int RandomMove() {
        int value = (int)(Random.value * 10);
        value = value % 9;
        value = value + 1;
        return value;
    }

    void Update()
    {
        
    }
}
