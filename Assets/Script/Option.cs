using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour{
    [System.Serializable]
    public class Setting{
        public bool playBGM, playSE, playTypeSound, playMissSound;
    }
    private string json;
    private Setting setting;
    [SerializeField] private Toggle bgm, se, type, miss;
    void OnEnable(){
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
        bgm.isOn = setting.playBGM;
        se.isOn = setting.playSE;
        type.isOn = setting.playTypeSound;
        miss.isOn = setting.playMissSound;
    }
    void OnDisable(){
        setting.playBGM = bgm.isOn;
        setting.playSE = se.isOn;
        setting.playTypeSound = type.isOn;
        setting.playMissSound = miss.isOn;
        json = JsonUtility.ToJson(setting);
        PlayerPrefs.SetString("saveData", json);
    }
}
