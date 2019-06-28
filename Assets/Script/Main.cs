using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

public class Main : MonoBehaviour {
    [System.Serializable]
    public class TypingData{
        public string[] words;
    }
    [SerializeField] private TypingData data;
    [SerializeField] private char[] question;
    [SerializeField] private GameObject[] prefabs;
    private int index;
    private bool isShift;
    private string[] keys =  {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z","1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
    private string input;
    private string inputLog;
    [SerializeField] private int correctCount, missCount;
    [SerializeField] private float correctRate, inputRate;
    [Header("UI")]
    [SerializeField] private Text inputText;
    [SerializeField] private Text questionText;
    [SerializeField] private Text rateText;
    [SerializeField] private Text secText;
    private void Awake(){
        //json読み込み
        string filePath = Application.dataPath + "/Data/data.json";
        string json = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<TypingData>(json);
        CreateQuestion();
    }
    private void Update(){
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            isShift = true;
        }else{
            isShift = false;
        }

        foreach(string key in keys){
            if(Input.GetKeyDown(key)){
                input = key;
                if(isShift) input = input.ToUpper();
                if(question[index].ToString() == input) Correct(input);
                else Mistake(input);
            }
        }

        //UI
        inputRate = correctCount / Time.time;
        secText.text = string.Format("{0:f1}key / sec", inputRate);
        if((missCount + correctCount) <= 0) return;
        correctRate = ((float)correctCount / (missCount + correctCount)) * 100;
        rateText.text = string.Format("{0:f} %", correctRate);
        
    }
    private void CreateQuestion(){
        index = 0;
        inputText.text = "";
        inputLog = "";
        question = data.words[Random.Range(0, data.words.Length)].ToCharArray();
        questionText.text = new string(question);
    }

    private void Correct(string key){
        index++;
        inputLog += key;
        inputText.text = inputLog; 
        correctCount++;
        if(index >= question.Length){
            for(int i = 0; i < question.Length; i++){
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(Random.Range(-8f, 8f), 5, 0), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
                StartCoroutine(Wait(15f, () => { Destroy(obj); }));
            }
            CreateQuestion();
        }
    }
    private void Mistake(string key){
        inputText.text = string.Format("{0}<color=#ff0000ff>{1}</color>", inputLog, key);
        missCount++;
    }

    IEnumerator Wait(float time, UnityAction action){
        yield return new WaitForSeconds(time);
        action();
    }
}