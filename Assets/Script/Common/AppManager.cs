using UnityEngine;

public class AppManager : SingletonMonoBehaviour<AppManager> {
    // 全体の初期化関連
    private void Awake() {
        Question.Initialize();
        Settings.Initialize();
        EffectManager.Initialize();
        ViewManager.Open(Const.View.Title, false);
    }
}