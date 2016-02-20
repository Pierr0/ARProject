using UnityEngine;
using System.Collections;

public class RessourceScript : MonoBehaviour {

    private bool statusRessource;

    [SerializeField]
    private int idTypeRessources;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool getStatusRessource()
    {
        return statusRessource;
    }

    public void setStatusRessource(bool status)
    {
        statusRessource = status;
    }

    public void refreshStatus()
    {
        this.gameObject.SetActive(statusRessource);
    }
}
