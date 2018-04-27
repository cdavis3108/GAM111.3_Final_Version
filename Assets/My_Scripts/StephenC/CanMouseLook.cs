﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMouseLook : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject character;

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        var mouseDeltaMove = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseDeltaMove = Vector2.Scale(mouseDeltaMove, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDeltaMove.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDeltaMove.y, 1f / smoothing);

        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f , 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}
