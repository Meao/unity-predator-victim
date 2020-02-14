using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBact : MonoBehaviour
{
    public GameObject Bacterius;
    GameObject BacteriusClone;

    // Start is called before the first frame update
    void Start()
    {
        BacteriusClone = Instantiate(Bacterius, transform.position, transform.rotation) as GameObject;
        BacteriusClone.name = "Bacterius";
        BacteriusClone.tag = "Bact";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("d"))
        {
            BacteriusClone = Instantiate(Bacterius, transform.position, transform.rotation) as GameObject;
        }
    }
}
