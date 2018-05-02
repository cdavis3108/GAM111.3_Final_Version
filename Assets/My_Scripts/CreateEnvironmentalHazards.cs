using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironmentalHazards : MonoBehaviour {

    public bool levelOne;
    public int numberOfHazards;
    public GameObject hazardPrefab;

    void OnEnable()
    {
        EventManager.OnClicked += CreateHazard;
    }

    void CreateHazard()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < numberOfHazards; i++)
        {
            if (levelOne)
            {
                pos.x += Random.Range(-120f, 120f);
                pos.z += Random.Range(-120f, 120f);
            }
            else
            {
                pos.x += Random.Range(-130f, 130f);
                pos.z += Random.Range(-40f, 40f);
            }
            GameObject hazard = Instantiate(hazardPrefab, pos, transform.rotation);
            Destroy(hazard, 4f);
        }
    }
}
