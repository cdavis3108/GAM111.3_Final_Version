using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Target : MonoBehaviour
{

    public float DragDistance = 5f;

    public GameObject item;
    public GameObject tempParent;
    public Transform guid;

    public Transform RadialBar;
    public Transform RadialText;

    // damage bar
    public Image energyBar;
    public Image healthBar;
    public float energyMax = 0f;
    public float healthMax = 100f;
    private float energyValue;
    private float healthValue;
    int interval = 1;
    float nextTime = 0;
    public int scoreValue = 1;

    public Animator MyAnimator { get; private set; }
    public GameObject hoverLight;
    public Rigidbody rb;

    public UpdateEnemies updateEnemiesSctipt;
    public GameObject gameManager;
    public DifficultySetting getdifficultysetting;
    public GameObject difficultySetter;

    void Start()
    {
        if(gameObject.tag != "Player")
        {
            MyAnimator = GetComponent<Animator>();
        }
        rb = GetComponent<Rigidbody>();
        //energyValue = energyMax;
        healthValue = healthMax;
        //Debug.Log(" - - - - -");
        gameManager = GameObject.FindGameObjectWithTag("GameManger");
        updateEnemiesSctipt = gameManager.GetComponent<UpdateEnemies>();
        difficultySetter = GameObject.FindGameObjectWithTag("DifficultySetting");

        if (difficultySetter != null)
        {
            getdifficultysetting = difficultySetter.GetComponent<DifficultySetting>();

            healthMax = getdifficultysetting.maxHealth;
        }
        else
        {
            healthMax = 50f;
        }
    }

    void Update()
    {
        if (healthBar != null)
        {


            //Debug.Log(Time.time);
            /*
            if (Time.time >= nextTime)
            {
                if (energyValue < energyMax)
                energyValue += 5;
                if (energyBar != null) energyBar.transform.localScale = new Vector3((energyValue / 100), 1, 1);

                    nextTime += interval;
                
            }
            */
        }

    }

    // energy and damage
    public void TakeDamage(float damageTake)
    {
        if (gameObject.tag != "Dead")
        {
            energyValue -= damageTake;
            if (energyValue <= 0f) RemoveBar(); //destroy object

            if (energyBar != null) energyBar.transform.localScale = new Vector3((energyValue / 100), 1, 1);

            if (RadialText != null)
            {
                RadialText.GetComponent<Text>().text = ((float)energyValue).ToString() + "%";
                RadialBar.GetComponent<Image>().fillAmount = energyValue / 100;
            }

            if (energyValue <= 0f)
            {
                healthValue -= damageTake;
                if (healthValue <= 0f)
                {
                    Destroy(healthBar);
                    Die();
                    self_Remove(); //destroy object
                }

                if (healthBar != null) healthBar.transform.localScale = new Vector3((healthValue / 100), 1, 1);
            }
        }
    }

    void RemoveBar()
    {
        Destroy(energyBar);
    }

    void Die()
    {
        if (gameObject.tag == "AI")
        {
            Debug.Log("AI is dying");
            MyAnimator.SetBool("hasTarget", false);
            MyAnimator.SetBool("powerDown", true);
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(new Vector3(Random.Range(10, 50), 0, 0));
            hoverLight.SetActive(false);
            gameObject.GetComponent<AIMedium>().enabled = false;
            gameObject.GetComponent<FieldOfView>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void self_Remove()
    {
        ScoreManager.score += scoreValue;
        Debug.Log(ScoreManager.score);
        updateEnemiesSctipt.FindEnemiesLeft();

        Destroy(gameObject, 5f);
    }
}
