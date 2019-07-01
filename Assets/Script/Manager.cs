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
    [SerializeField] private GameObject[] prefabs;
    public TypingData data;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        //json読み込み
        string filePath = Application.dataPath + "/Data/data.json";
        string json = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<TypingData>(json);
        data.words = data.words.OrderBy((content) => content.Length).ToArray();
    }

    public GameObject CreatePrefab(){
        return Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
    }
}
