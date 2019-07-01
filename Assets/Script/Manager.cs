using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Manager : SingletonMonoBehaviour<Manager>{
    [System.Serializable]
    public class TypingData{
        public string[] words;
    }
    public TypingData data;
    void Awake(){
        //json読み込み
        string filePath = Application.dataPath + "/Data/data.json";
        string json = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<TypingData>(json);
        data.words = data.words.OrderBy((content) => content.Length).ToArray();
    }
}
