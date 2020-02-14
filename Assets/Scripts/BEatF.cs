using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEatF : MonoBehaviour
{
    public string Tag;
    public float Grow, Dry;
    Vector3 Size;
    public GameObject Bacterius;
    GameObject BacteriusClone;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tag)
        {
            transform.localScale += new Vector3(Grow, 0f, Grow);
            Destroy(other.gameObject);
        }
    }
    
    void Update()
    {
        transform.localScale += new Vector3(Dry, 0f, Dry);

        Size = GetComponent<Collider>().bounds.size;
        if(Size.x < 0.001f)
        {
            Destroy(gameObject);
        }
        else if(Size.x > 0.5)
        {
            transform.localScale += new Vector3(-0.4f, 0f, -0.4f);
            //To do: create 2 smaller ones and destroy the old one or reduce size and create a new one?
            BacteriusClone = Instantiate(Bacterius, transform.position, transform.rotation) as GameObject;
            BacteriusClone.name = "Bacterius";
            BacteriusClone.tag = "Bact";
        }
    }
}
