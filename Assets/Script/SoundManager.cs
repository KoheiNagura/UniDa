using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>{
    [System.Serializable]
    public class Setting{
        public bool playBGM, playSE, playTypeSound, playMissSound;
    }

    [SerializeField] private AudioSource bgm, correct, miss, start, next;
    public Setting setting;
    private string json;
    void Start(){
        json = PlayerPrefs.GetString("saveData");
        print(json);
        if(json == ""){
            setting = new Setting();
            setting.playBGM = true;
            setting.playSE = true;
            setting.playTypeSound = true;
            setting.playMissSound = true;
        }else{
            setting = JsonUtility.FromJson<Setting>(json);
        }
        Reload();
    }
    public void Reload(){
        if(!setting.playBGM) bgm.Stop();
        else PlayBGM();
    }
    public void SaveSetting(){
        json = JsonUtility.ToJson(setting);
        PlayerPrefs.SetString("saveData", json);
    }
    public void PlayBGM(){
        if(setting.playBGM) bgm.Play();
    }
    public void PlayCorrect(){
        if(setting.playTypeSound) correct.Play();
    }
    public void PlayMiss(){
        if(setting.playMissSound) miss.Play();
    }
    public void PlayStart(){
        if(setting.playSE) start.Play();
    }
    public void PlayNext(){
        if(setting.playSE) next.Play();
    }
}
