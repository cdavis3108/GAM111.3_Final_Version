using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetting : MonoBehaviour {

    private bool created = false;

    public float maxHealth;
    public float damage;

    private void Awake()
    {
        if(!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    public void EasyDifficulty ()
    {
        maxHealth = 30f;
        damage = 5f;
    }

    public void MediumDifficulty()
    {
        maxHealth = 50f;
        damage = 10f;
    }

    public void HardDifficulty()
    {
        maxHealth = 70f;
        damage = 20f;
    }
}
