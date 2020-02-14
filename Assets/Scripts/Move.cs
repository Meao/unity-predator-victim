using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float xPos, zPos, speed = 2f;
    Vector3 desiredPos, dirNormalized;
    public GameObject[] food;

    void Start()
    {
        xPos = Random.Range(-0.5f, 0.5f);
        zPos = Random.Range(-0.5f, 0.5f);
        desiredPos = new Vector3(xPos, transform.position.y, zPos);
        dirNormalized = (desiredPos - transform.position).normalized;

        //Debug.DrawRay(transform.position, dirNormalized, Color.green);
        // It works, but they go through walls in space
        RaycastHit hit;
        bool tRay;
        tRay = Physics.Raycast(transform.position, dirNormalized, out hit, 5);
        if(tRay)
        {
          if(hit.collider.tag == "Am")
          {
            Debug.Log("Amoeba found");
            desiredPos = new Vector3(-xPos * 0.1f, transform.position.y, -zPos * 0.1f);
            dirNormalized = (desiredPos - transform.position).normalized;
          }
        }
      }

    void Update()
    {
        food = GameObject.FindGameObjectsWithTag("Food");
        int i = Random.Range(0, food.Length - 1);
        // that works ok but with acceleration transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
        transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
          {
          desiredPos = food[i].transform.position;
          dirNormalized = (desiredPos - transform.position).normalized;
          }

        RaycastHit hit;
        bool tRay;
        tRay = Physics.Raycast(transform.position, dirNormalized, out hit, 5);
        if(tRay)
        {
          if(hit.collider.tag == "Am")
          {
            Debug.Log("Amoeba found");
            //desiredPos = new Vector3(-xPos * 0.1f, transform.position.y, -zPos * 0.1f);
            desiredPos = new Vector3(dirNormalized.x * (-1f), dirNormalized.y * (-1f), dirNormalized.z * (-1f));
            dirNormalized = (desiredPos - transform.position).normalized;
          }
        }
      }
}
