using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour{
    [SerializeField] private Toggle bgm, se, type, miss;
    void OnEnable(){
        bgm.isOn = SoundManager.Instance.setting.playBGM;
        se.isOn = SoundManager.Instance.setting.playSE;
        type.isOn = SoundManager.Instance.setting.playTypeSound;
        miss.isOn = SoundManager.Instance.setting.playMissSound;
    }
    void OnDisable(){
        SoundManager.Instance.setting.playBGM = bgm.isOn;
        SoundManager.Instance.setting.playSE = se.isOn;
        SoundManager.Instance.setting.playTypeSound = type.isOn;
        SoundManager.Instance.setting.playMissSound = miss.isOn;
        SoundManager.Instance.SaveSetting();
        SoundManager.Instance.Reload();
    }
}
