using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class Main : MonoBehaviour {
    [SerializeField] private char[] question;
    private int index;
    private bool isShift;
    private string[] keys =  {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z","1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
    private string input, inputLog;
    private int logMax = 5;
    private List<string> questionLog = new List<string>();
    private int nowLevel;
    private List<string> level1 = new List<string>();
    private List<string> level2 = new List<string>();
    private List<string> level3 = new List<string>();
    private List<string> level4 = new List<string>();
    [SerializeField] private int correctCount, missCount;
    [SerializeField] private float correctRate, inputRate;
    [Header("UI")]
    [SerializeField] private Text inputText, questionText, guideText, rateText, secText;
    private void Start(){
        //文字数でレベル分けする。
        foreach(string s in Manager.Instance.data.words){
            if(s.Length > 13){
                level4.Add(s);
            }else if(s.Length > 9){
                level3.Add(s);
            }else if(s.Length > 5){
                level2.Add(s);
            }else{
                level1.Add(s);                
            }
        }
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
        List<string> array = new List<string>();
        if(nowLevel < 4){
            array = level1;
        }else if(nowLevel < 9){
            array = level2;
        }else if(nowLevel < 13){
            array = level3;
        }else{
            array = level4;
        }
        index = 0;
        inputText.text = "";
        inputLog = "";
        string s = array[Random.Range(0, array.Count)];
        while(questionLog.Contains(s)){
            s = array[Random.Range(0, array.Count)];
        }
        questionLog.Add(s);
        if(questionLog.Count > logMax) questionLog.RemoveAt(0);
        question = s.ToCharArray();
        questionText.text = new string(question);
        guideText.text = questionText.text;
        nowLevel++;
        if(nowLevel >= 16) nowLevel = 0;
    }

    private void Correct(string key){
        index++;
        inputLog += key;
        inputText.text = inputLog; 
        correctCount++;
        if(index >= question.Length){
            for(int i = 0; i < question.Length; i++){
                GameObject obj = Manager.Instance.CreatePrefab();
                obj.transform.position = new Vector3(Random.Range(-8f, 8f), 5, 0);
                obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
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