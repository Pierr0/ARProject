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

    [SerializeField]
    private float timeBetweenAttacks = 10.0f;

    private float timerAttacks = 0.0f;

    private bool clockActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (clockActive)
            ClockAttacks();
	}

    public void gameStart()
    {
        golem.setEnable();
        demon.setEnable();
        startButton.SetActive(false);
        clockActive = true;
    }

    private void ClockAttacks()
    {
        timerAttacks += Time.deltaTime;
        if (timerAttacks > timeBetweenAttacks)
        {
            golem.damageGolem(demon.getElementInfused());
            Debug.Log("hit");
            timerAttacks = 0.0f;
        }
    }

    public void endGame()
    {
        clockActive = false;
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
