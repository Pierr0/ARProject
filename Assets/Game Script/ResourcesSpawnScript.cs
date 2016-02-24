using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesSpawnScript : MonoBehaviour
{
    public List<GameObject> RessourceSpawnable;

    public float timeToRespawn = 3.0f;
    private float timerRespawn = 0.0f;   

    // Use this for initialization
    void Start()
    {
        ResetRessources();
        RandomRessources();
    }

    // Update is called once per frame
    void Update()
    {        
        ClockRespawn();
    }

    private void ClockRespawn()
    {
        //GameObject temp;
        timerRespawn += Time.deltaTime;
        if (timerRespawn > timeToRespawn)
        {
            ResetRessources();
            RandomRessources();
            timerRespawn = 0.0f;
        }
    }

    private void ResetRessources()
    {
        for (int i = 0; i < RessourceSpawnable.Count; i++)
            ChangeObjectLocation(RessourceSpawnable[i], false);
    }

    private void RandomRessources()
    {
        int index = Random.Range(0, RessourceSpawnable.Count);
        ChangeObjectLocation(RessourceSpawnable[index], true);        
    }

    private void ChangeObjectLocation(GameObject obj, bool status)
    {        
        Vector3 newPos = -100*Vector3.one;
        obj.transform.position = status ? transform.position : newPos;        
    }
}