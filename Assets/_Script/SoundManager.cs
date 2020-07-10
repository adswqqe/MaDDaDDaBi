using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMSound
{
    BGM_Title = 0,
    BGM_inStore,
    BGM_inUnderG,
};

public enum BGMEffSound
{
    AMB_Forest = 0,
    AMB_UnderG,
};

public enum EffSound
{
    SFX_UI_start = 0,
    SFX_UI_Bookmenu,
    SFX_UI_Boardmenu,
    SFX_UI_button,
    SFX_UI_okay,
    SFX_UI_cancle,
    SFX_UI_open,
    SFX_UI_sleep,
    SFX_UI_setting,
    SFX_UI_handleCoin,
    SFX_UI_request,
    SFX_UI_order,
    SFX_UI_D_potion,
    SFX_UI_D_arm,
    SFX_UI_D_acc,
    SFX_UI_B_pos,
    SFX_UI_B_okay,
    SFX_UI_B_cancle,
    SFX_UI_C_popping,
    SFX_UI_C_M,
    SFX_UI_C_suc,
};

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [FMODUnity.EventRef]
    public List<string> BGMSounds;
    [FMODUnity.EventRef]
    public List<string> BGMEffSounds;
    [FMODUnity.EventRef]
    public List<string> EffSounds;
    FMOD.Studio.EventInstance BGMSoundEvent;
    FMOD.Studio.EventInstance BGMEffSoundEvent;
    FMOD.Studio.EventInstance EffEvent;
    FMOD.Studio.EventInstance ProEvent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BGMSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMSounds[(int)BGMSound.BGM_inUnderG]);
        BGMSoundEvent.start();
        BGMEffSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMEffSounds[(int)BGMEffSound.AMB_UnderG]);
        BGMEffSoundEvent.start();
    }

    private void Update()
    {
    }

    public void PlayBGM(BGMSound bgm)
    {
        BGMSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BGMSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMSounds[(int)bgm]);
        BGMSoundEvent.start();
    }

    public void PlayBGM(int bgm)
    {
        BGMSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BGMSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMSounds[(int)bgm]);
        BGMSoundEvent.start();
    }

    public void PlayEffBgm(BGMEffSound bgmeff)
    {
        BGMEffSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BGMEffSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMEffSounds[(int)bgmeff]);
        BGMEffSoundEvent.start();
    }

    public void PlayEffBgm(int bgmeff)
    {
        BGMEffSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BGMEffSoundEvent = FMODUnity.RuntimeManager.CreateInstance(BGMEffSounds[(int)bgmeff]);
        BGMEffSoundEvent.start();
    }

    public void PlayEff(EffSound eff)
    {
        EffEvent = FMODUnity.RuntimeManager.CreateInstance(EffSounds[(int)eff]);
        EffEvent.start();
    }

    public void PlayEff(int eff)
    {
        EffEvent = FMODUnity.RuntimeManager.CreateInstance(EffSounds[(int)eff]);
        EffEvent.start();
    }

    public void PlayProductionEff()
    {
        ProEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ProEvent = FMODUnity.RuntimeManager.CreateInstance(EffSounds[18]);
        ProEvent.start();
    }

    public void StopProductionEff()
    {
        ProEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Debug.Log("stop");
    }
    
    public void StopBGM()
    {
        BGMSoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
