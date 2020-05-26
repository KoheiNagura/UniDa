using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager> {
    [SerializeField] private AudioSource bgmSource, seSource;
    [SerializeField] private AudioClip defaultBGM, defaultSE;

    public static void PlayBGM(AudioClip clip = null) {
        if(!Settings.PlayBGM) return;
        if(clip == null) clip = Instance.defaultBGM;
        Instance.bgmSource.clip = clip;
        Instance.bgmSource.Play();
    }

    public static void PlayOneShot(AudioClip clip = null) {
        if(!Settings.PlaySE) return;
        if(clip == null) clip = Instance.defaultSE;
        Instance.seSource.PlayOneShot(clip);
    }
    
    public static void UpdateVolume(float bgm, float se) {
        Instance.bgmSource.volume = bgm;
        Instance.seSource.volume = se;
    }
}