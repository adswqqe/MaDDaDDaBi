using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string BGMSound;
    [FMODUnity.EventRef]
    public string EffSound;
    FMOD.Studio.EventInstance soundEvent;
    FMOD.Studio.EventInstance EffEvent;

    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMSound);
        EffEvent = FMODUnity.RuntimeManager.CreateInstance(EffSound);
        soundEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            EffEvent.start();
            SceneManager.LoadScene(1);
        }
    }
}
