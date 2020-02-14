using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAm : MonoBehaviour
{
    public GameObject Amoeba;
    GameObject AmoebaClone;

    // Start is called before the first frame update
    void Start()
    {
        AmoebaClone = Instantiate(Amoeba, transform.position, transform.rotation) as GameObject;
        Amoeba.name = "Amoeba";
        Amoeba.tag = "Am";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            AmoebaClone = Instantiate(Amoeba, transform.position, transform.rotation) as GameObject;
        }
    }
}
