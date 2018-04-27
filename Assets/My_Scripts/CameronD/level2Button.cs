using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2Button : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(3);
    }
}
