using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEatB : MonoBehaviour
{
    public string Tag;
    public float Grow, Dry;
    Vector3 Size;
    
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
        transform.localScale -= new Vector3(Dry, 0f, Dry);

        Size = GetComponent<Collider>().bounds.size;
        if(Size.x < 0.001f)
        {
            Destroy(gameObject); //Doesn't work
        }
    }
}
