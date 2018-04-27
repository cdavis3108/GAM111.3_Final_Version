using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFace : MonoBehaviour {

    public GameObject tempParent;
    public GameObject bar;

    private void Start()
    {
        tempParent = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update () {
        bar.transform.rotation = tempParent.transform.rotation;
    }
}
