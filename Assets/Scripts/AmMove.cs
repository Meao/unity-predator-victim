using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmMove : MonoBehaviour
{
    public float speed = .00001f;
    public GameObject[] food;
    Vector3 amSize;
    Collider amCollider;

    Transform tr_bact;
    float f_S = 3f;

    // Update is called once per frame
    void Update()
    {
        amCollider = GetComponent<Collider>();
        amSize = amCollider.bounds.size;
        if(amSize.x < 1.5)
        {
            food = GameObject.FindGameObjectsWithTag("Bact");
            if(food.Length > 3)
            {
                int i = Random.Range(0, food.Length - 1);
                tr_bact = food[i].transform;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_bact.position - transform.position), f_S * Time.deltaTime);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }
}
