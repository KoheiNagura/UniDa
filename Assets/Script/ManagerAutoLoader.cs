using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerAutoLoader {
    //ゲーム開始時に実行される
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene(){
        string sceneName = "ManagerScene";
        //すでにManagerSceneが読み込まれていないか
        if(!SceneManager.GetSceneByName(sceneName).IsValid()){
            SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
        }
    }
}
