using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Player;

    public float DragDistance = 5f;

    public GameObject item;
    public GameObject tempParent;
    public Transform guid;

    public Transform RadialBar;
    public Transform RadialText;

    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    public AudioSource playerAudio;
    CharacterController CharacterControler;
    bool damaged;

    // damage bar
    public Image healthBar;
    public float healthMax = 100f;
    private float healthValue;
    public Rigidbody rb;

    // Find enemies
    public GameObject[] enemies;

    void Start()
    {
        healthValue = healthMax;
        //Debug.Log(" - - - - -");
        //playerAudio = GetComponentInChildren<AudioSource>();
        CharacterControler = GetComponent<CharacterController>();

        Player.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    // energy and damage
    public void TakeDamage(float damageTake)
    {
        //Debug.Log("Player taking damage");
        damaged = true;
        healthValue -= damageTake;

        playerAudio.Play();

        if (healthBar != null) healthBar.transform.localScale = new Vector3((healthValue / 100), 1, 1);

        damaged = false;

        if (healthValue <= 0f) Die(); //destroy object
    }

  
    void Die()
    {
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //gameObject.GetComponent<CharacterControler>().enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        //Destroy(gameObject);
        //Player.gameObject.SetActive(false);\
        //GetComponent<RigidbodyFirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
         SceneManager.LoadScene(5);
        
    }
}