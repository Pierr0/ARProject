using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesSpawnScript : MonoBehaviour {

    [SerializeField]
    private List <RessourceScript> RessourceSpawnable;

    [SerializeField]
    private float timeToRespawn = 30.0f;

    private float timerRespawn = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ClockRespawn();
	}

    private void ClockRespawn()
    {
        timerRespawn += Time.deltaTime;
        if (timerRespawn > timeToRespawn)
        {
            ResetRessources();
            RandomRessources();
            timerRespawn = 0.0f;
        }
    }

    private void RandomRessources()
    {
        int index = Random.Range(0, RessourceSpawnable.Count);
        RessourceSpawnable[index].setStatusRessource(true);
        RessourceSpawnable[index].refreshStatus();
        Debug.Log("random ressource : " + RessourceSpawnable[index].getIdTypeRessources());
    }

    private void ResetRessources()
    {
        for (int i = 0; i < RessourceSpawnable.Count; i++)
        {
            if(RessourceSpawnable[i].getStatusRessource() == true){
                RessourceSpawnable[i].setStatusRessource(false);
                RessourceSpawnable[i].refreshStatus();
            }
        }
    }

}
