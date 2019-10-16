using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    // Start is called before the first frame update
    public Material dark;
    public Material soft;
    public bool flag = true;
    public GameObject struct0;
    public GameObject struct1;
    public GameObject struct2;
    public GameObject struct3;
    private float cooldown = 0.5f;
    private float latest = 0;
    //GameObject[] structs = new GameObject[] { struct0, struct1, struct2, struct3 };



    void Start()
    {

    }

    private void switchMaterial(GameObject gb, bool change) {

        if (flag)
        {
            gb.GetComponent<Renderer>().material = soft;
        }
        else {
            gb.GetComponent<Renderer>().material = dark;
        }
        if (change) {
            flag = !flag;
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        bool in_time = cooldown < (Time.time -latest);
        if (Input.GetKey(KeyCode.T) && in_time) {
            switchMaterial(struct0, false);
            switchMaterial(struct1, false);
            switchMaterial(struct2, false);
            switchMaterial(struct3, true);
            latest = Time.time;
        }
        
    }
}
