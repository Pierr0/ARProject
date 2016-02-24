using UnityEngine;
using System.Collections;

public class RessourceScript : MonoBehaviour {

    public int idTypeRessources;

    private bool statusRessource;

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

    public int getIdTypeRessources(){
        return idTypeRessources;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
