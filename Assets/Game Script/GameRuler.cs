using UnityEngine;
using System.Collections;

public class GameRuler : MonoBehaviour {

    private int idRessourceHolded;

    [SerializeField]
    private PlayerHealth golem;

    [SerializeField]
    private PlayerHealth demon;

    [SerializeField]
    private GameObject startButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void gameStart()
    {
        golem.setEnable();
        demon.setEnable();
        //startButton.setEnable();
    }

    public void endGame()
    {
        if (golem.getDeath() == true)
        {
            //Lose
        }
        else if (demon.getDeath() == true)
        {
            //win
        }
    }
}
