using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView {
    public Const.View viewType => Const.View.Title;
    public Transform origin => transform;
    [SerializeField] private Button playButton, configButton;

    private void Awake() {
        playButton.onClick.AddListener( () => {
            ViewManager.Open(Const.View.Game);
            AudioManager.PlayOneShot();
        });
        configButton.onClick.AddListener( ()=>{
            ViewManager.Open(Const.View.Option, false);
            AudioManager.PlayOneShot();
        });
        AudioManager.PlayBGM();
    }

    public void Open() { }

    public void Close() => EffectManager.Hide();
    
    private void Update() {
        if(Input.anyKeyDown) {
            GameObject effect = EffectManager.GetRandomEffect();
            effect.transform.position = new Vector3(Random.Range(-8f, 8f), 5, 0);
            effect.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        }
    }
}
