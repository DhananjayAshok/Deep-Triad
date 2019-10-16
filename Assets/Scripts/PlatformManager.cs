using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject child;
    public GameObject above;
    private GameObject piece;
    void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        child.SetActive(true);
        if (above != null) {
            above.SetActive(true);
        }
        piece = other.gameObject;
    }

    public void Reset()
    {
        if (above == null)
        {
            child.SetActive(false);
            Destroy(piece);
        }
        else {
            PlatformManager pm = above.GetComponent<PlatformManager>();
            if (pm.child.activeSelf)
            {
                pm.Reset();
            }
            else {
                child.SetActive(false);
                above.SetActive(false);
                Destroy(piece);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
