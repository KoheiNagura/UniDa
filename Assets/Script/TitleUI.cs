using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : UIBase{
    void Update(){
        if(Input.anyKeyDown){
            GameObject obj = Manager.Instance.CreatePrefab();
            obj.transform.position = new Vector3(Random.Range(-8f, 8f), 5, 0);
            obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        }
    }
}
