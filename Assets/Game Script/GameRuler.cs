using UnityEngine;
using System.Collections;

public class GameRuler : MonoBehaviour {

    static int idRessourceHolded = -1;

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
        CheckPlayerInput();
    }

    // Check player input
    private void CheckPlayerInput()
    {    
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hitInfo;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Specify the ray to be casted from the position of the mouse click

            // Raycast and verify that it collided
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("mouse hit!");
                // Select the object if it has the good Tag
                if (hitInfo.collider.gameObject.tag == ("Marker"))
                {
                    idRessourceHolded = hitInfo.collider.gameObject.GetComponent<ResourcesSpawnScript>().CurrentRessourceID();
                    Debug.Log("Marker = " + idRessourceHolded);
                }
                if (hitInfo.collider.gameObject.tag == ("Demon"))
                {
                    attack();

                }
                if (hitInfo.collider.gameObject.tag == ("Golem"))
                {
                    shielding();
                }
            }
        }
    }

    public void attack()//si on clique sur le demon
    {
        if (idRessourceHolded != -1)
        {
            demon.damageDemon(idRessourceHolded);
        }
    }

    public void shielding()//si on clique sur le golem
    {
        if (idRessourceHolded != -1)
        {
            golem.addShield(idRessourceHolded);
        }
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
