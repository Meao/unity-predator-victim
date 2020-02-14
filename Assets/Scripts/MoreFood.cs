using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class MoreFood : MonoBehaviour
{
    public GameObject[] foodSpots;
    public Vector3 spawnValues; //To do: make it get dish bottom x value -2 (or half the size of a food spot) and /2
    public float spawnWait, spawnMostWait, spawnLeastWait;
    int randFood;
    public bool stop;

    void Awake () 
    {
        Debug.Log(DateTime.Now);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        while (!stop)
        {
            randFood = Random.Range (0, 2); //To do: make it get the number of foodSpots. Error if I use foodSpots.Count is 'GameObject[]' does not contain a definition for 'Count' and no accessible extension method 'Count' accepting a first argument of type 'GameObject[]' could be found (are you missing a using directive or an assembly reference?)
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), -0.1f, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(foodSpots[randFood], spawnPosition + transform.TransformPoint(0,0,0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait); //randomised in Update()
        }
    }
}
