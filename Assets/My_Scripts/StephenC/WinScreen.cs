using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class WinScreen : MonoBehaviour
    {
        public GameObject Player;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<RigidbodyFirstPersonController>().enabled = false;
                SceneManager.LoadScene(4);
            }
        }
    }
}
