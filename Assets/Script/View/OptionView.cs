using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionView : MonoBehaviour, IView {
    public Const.View viewType => Const.View.Option;
    public Transform origin => transform;
    [SerializeField] private Toggle
        bgmToggle,
        seToggle,
        typeSoundToggle,
        missSoundToggle;
    [SerializeField] private Button closeButton, background;
    private void Awake() {
        closeButton.onClick.AddListener( () => {
            ViewManager.Close(viewType);
            AudioManager.PlayOneShot();
        });
        background.onClick.AddListener( () => {
            ViewManager.Close(viewType);
            AudioManager.PlayOneShot();
        });
        bgmToggle.onValueChanged.AddListener( isOn => {
            Settings.PlayBGM = isOn;
            AudioManager.PlayOneShot();
            if(isOn) AudioManager.PlayBGM();
            else AudioManager.StopBGM();
        });
        seToggle.onValueChanged.AddListener( isOn => {
            Settings.PlaySE = isOn;
            AudioManager.PlayOneShot();
        });
        typeSoundToggle.onValueChanged.AddListener( isOn => {
            Settings.PlayTypeSound = isOn;
            AudioManager.PlayOneShot();
        });
        missSoundToggle.onValueChanged.AddListener( isOn => {
            Settings.PlayMissSound = isOn;
            AudioManager.PlayOneShot();
        });
    }

    public void Open() {
        bgmToggle.isOn = Settings.PlayBGM;
        seToggle.isOn = Settings.PlaySE;
        typeSoundToggle.isOn = Settings.PlayTypeSound;
        missSoundToggle.isOn = Settings.PlayMissSound;
    }

    public void Close() {
        if(!Settings.PlayBGM) AudioManager.StopBGM();
        Settings.Save();
    }
}
