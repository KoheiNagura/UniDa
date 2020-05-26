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
    public static bool PlayBGM => _i._playBGM;
    public static bool PlaySE => _i._playSE;
    public static bool PlayTypeSound => _i._playTypeSound;
    public static bool PlayMissSound => _i._playMissSound;

    public static void Initialize() => Load();

    public static void Save() {
        var json = JsonUtility.ToJson(_i);
        PlayerPrefs.SetString( Const.SaveKeys.Settings, json );
    }

    public static void Load() {
        var key = Const.SaveKeys.Settings;
        if(PlayerPrefs.HasKey(key)) {
            _i = JsonUtility.FromJson<Settings>( PlayerPrefs.GetString(key) );
        }else _i = new Settings();
    }
}