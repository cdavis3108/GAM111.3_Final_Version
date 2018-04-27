using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEnemies : MonoBehaviour {

    public GameObject[] enemies;
    public int enemies2;
    public GameObject exit;
    public bool changeEnemies;

    Text text;

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("AI");
        enemies2 = enemies.Length;
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Enemies: " + enemies2;
    }

    public void FindEnemiesLeft ()
    {
        --enemies2;
        // Update HUD
        if (enemies2 <= 0)
        {
            // Open Door
            Destroy(exit);
        }
    }
}
