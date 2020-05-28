using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Settings {
    private static Settings _i;
    [SerializeField] private bool
        _playBGM,
        _playSE,
        _playTypeSound,
        _playMissSound;
    public static bool PlayBGM {
        get { return _i._playBGM; }
        set { _i._playBGM = value; }
    }
    public static bool PlaySE {
        get { return _i._playSE; }
        set { _i._playSE = value; }
    }
    public static bool PlayTypeSound {
        get { return _i._playTypeSound; }
        set { _i._playTypeSound = value; }
    }
    public static bool PlayMissSound {
        get { return _i._playMissSound; }
        set { _i._playMissSound = value; }
    }

    public static void Initialize() => Load();

    public static void Save() {
        var json = JsonUtility.ToJson(_i);
        PlayerPrefs.SetString( Const.SaveKeys.Settings, json );
    }

    public static void Load() {
        var key = Const.SaveKeys.Settings;
        if(PlayerPrefs.HasKey(key)) {
            _i = JsonUtility.FromJson<Settings>( PlayerPrefs.GetString(key) );
        }else{
            _i = new Settings();
            PlayBGM = true;
            PlaySE = true;
            PlayTypeSound = true;
            PlayMissSound = true;
        }
    }
}