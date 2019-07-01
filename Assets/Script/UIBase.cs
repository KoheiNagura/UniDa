using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIBase : MonoBehaviour{
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeActive(GameObject obj){
        obj.SetActive(!obj.activeSelf);
    }
}
