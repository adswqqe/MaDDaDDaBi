using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public Animator transition;

    float transitionTime = 0.30f;


    public void OnStartSceneTransition(bool isEndDay)
    {
        if (isEndDay)
            StartCoroutine(StartSceneTransition());
    }

    IEnumerator StartSceneTransition()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    }
}
