using UnityEngine;
using UnityEngine.UI;

public class GameRuler : MonoBehaviour
{
    static int idRessourceHolded = -1;

    [SerializeField]
    private PlayerHealth golem;

    [SerializeField]
    private PlayerHealth demon;

    [SerializeField]
    private float timeBetweenAttacks = 10.0f;

    //Icon UI

    [SerializeField]
    private GameObject golemFireIcon;

    [SerializeField]
    private GameObject golemWaterIcon;

    [SerializeField]
    private GameObject golemEarthIcon;

    [SerializeField]
    private GameObject demonFireIcon;

    [SerializeField]
    private GameObject demonWaterIcon;

    [SerializeField]
    private GameObject demonEarthIcon;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private Text winText;
    //-----

    private float timerAttacks = 0.0f;

    private bool clockActive = false;

    private int attacksCount = 0;

    [SerializeField]
    private int nbAttackByElements = 3;
	
    void Start()
    {
        winText.gameObject.SetActive(false);
        restartButton.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if (clockActive)
            ClockAttacks();
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
#if UNITY_ANDROID

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                // Construct a ray from the current touch coordinates
                GetPlayerInput(Camera.main.ScreenPointToRay(Input.GetTouch(i).position));
            }
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            GetPlayerInput(Camera.main.ScreenPointToRay(Input.mousePosition)); // Specify the ray to be casted from the position of the mouse click

        }

#endif
    }

    // Check player input
    private void GetPlayerInput(Ray ray)
    {
        RaycastHit hitInfo;
        
        // Raycast and verify that it collided
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("mouse/Touch hit!");
            // Select the object if it has the good Tag
            if (hitInfo.collider.gameObject.tag == ("Marker"))
            {
                idRessourceHolded = hitInfo.collider.gameObject.GetComponent<ResourcesSpawnScript>().CurrentRessourceID();

                if (idRessourceHolded == 0)
                {
                    golemFireIcon.SetActive(true);
                    golemWaterIcon.SetActive(false);
                    golemEarthIcon.SetActive(false);
                }
                else if (idRessourceHolded == 1)
                {
                    golemFireIcon.SetActive(false);                    
                    golemEarthIcon.SetActive(true);
                    golemWaterIcon.SetActive(false);
                }
                else if (idRessourceHolded == 2)
                {
                    golemFireIcon.SetActive(false);                    
                    golemEarthIcon.SetActive(false);
                    golemWaterIcon.SetActive(true);
                }
            }
            if (hitInfo.collider.gameObject.tag == ("Demon"))
            {
                attack();

                golemFireIcon.SetActive(false);                
                golemEarthIcon.SetActive(false);
                golemWaterIcon.SetActive(false);

            }
            if (hitInfo.collider.gameObject.tag == ("Golem"))
            {
                shielding();

                golemFireIcon.SetActive(false);
                golemEarthIcon.SetActive(false);
                golemWaterIcon.SetActive(false);
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
        ChangeDemonElementIcon();
        startButton.SetActive(false);
        winText.gameObject.SetActive(false);
        restartButton.SetActive(false);
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
            attacksCount++;
            if (attacksCount >= nbAttackByElements)
            {
                ChangeDemonElementIcon();
                attacksCount = 0;
            }
        }
    }

    private void ChangeDemonElementIcon()
    {
        int elementDem = Random.Range(0, 3);
        demon.setElementInfused(elementDem);
        if (elementDem == 0)
        {
            demonFireIcon.SetActive(true);
            demonWaterIcon.SetActive(false);
            demonEarthIcon.SetActive(false);
        }
        else if (elementDem == 1)
        {
            demonFireIcon.SetActive(false);            
            demonEarthIcon.SetActive(true);
            demonWaterIcon.SetActive(false);
        }
        else if (elementDem == 2)
        {
            demonFireIcon.SetActive(false);
            demonEarthIcon.SetActive(false);
            demonWaterIcon.SetActive(true);            
        }
    }

    public void endGame()
    {
        clockActive = false;
        winText.gameObject.SetActive(true);
        if (golem.getDeath() == true)
        {
            //Lose
            winText.text = "ENEMIE A GAGNÉ !!!";
            winText.color = Color.red;
        }
        else if (demon.getDeath() == true)
        {
            //win
            winText.text = "VOUS AVEZ GAGNÉ !!!";
            winText.color = Color.blue;
        }
        restartButton.SetActive(true);
    }
}