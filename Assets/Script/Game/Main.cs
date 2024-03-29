// 戒めのために残している旧コード
// いらんやつら消したのでエラーだらけです
// リファクタ時にコメントちょっとかいた。

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;

// public class Main : MonoBehaviour {
//     [SerializeField] private char[] question;
//     private int index;
//     private bool isShift;
//     private string[] keys =  {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z","1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
//     private string input, inputLog;
//     private int logMax = 5;
//     private List<string> questionLog = new List<string>();
//     private int nowLevel;
//     private List<string> level1 = new List<string>();
//     private List<string> level2 = new List<string>();
//     private List<string> level3 = new List<string>();
//     private List<string> level4 = new List<string>();
//     [SerializeField] private GameObject hideObject;
//     public static int correctCount, missCount;
//     public static int score;
//     private float timer = 60;
//     private bool isPlaying, flag, noMiss;
//     [Header("UI")]
//     [SerializeField] private Text inputText, questionText, guideText, scoreText, timerText;
//     private float lastScore = 0, displayScore = 0;
//     private float diff, counting;
//     private void Start(){
//         score = 0;
//         correctCount = 0;
//         missCount = 0;
//         //文字数でレベル分けする。
//         foreach(string s in Manager.Instance.data.words){
//             if(s.Length > 13){
//                 level4.Add(s);
//             }else if(s.Length > 9){
//                 level3.Add(s);
//             }else if(s.Length > 5){
//                 level2.Add(s);
//             }else{
//                 level1.Add(s);                
//             }
//         }
//         CreateQuestion();
//     }
//     private void Update(){
//         if(isPlaying){
//             if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
//                 isShift = true;
//             }else{
//                 isShift = false;
//             }
//             //入力
//             foreach(string key in keys){
//                 if(Input.GetKeyDown(key)){
//                     input = key;
//                     if(isShift) input = input.ToUpper();
//                     if(question[index].ToString() == input) Correct(input);
//                     else Mistake(input);
//                 }
//             }
//             // ! UI部分は分割する
//             //UI
//             if(lastScore < score){
//                 lastScore = score;
//                 diff = lastScore - displayScore;
//                 counting = diff;
//             }
//             if(diff > 0){
//                 displayScore += counting * Time.deltaTime * 2;
//                 diff -= counting * Time.deltaTime * 2;
//                 if(displayScore > lastScore) displayScore = lastScore;
//             }
//             scoreText.text = "SCORE" + "\n" + Mathf.CeilToInt(displayScore).ToString("D5");
//             timerText.text = "TIME" + "\n" + Mathf.FloorToInt(timer).ToString("D2");

//             timer -= Time.deltaTime;
//             //時間切れになったら表記整えてる。 ここTimeUp()とかでまとめたら可読性よさそう
//             if(timer < 0 && isPlaying){
//                 isPlaying = false;
//                 scoreText.text = "SCORE" + "\n" + score.ToString("D5");
//                 timerText.text = "TIME" + "\n" + 0.ToString("D2");
//                 SceneManager.LoadScene("Result");
//             }
//         }else{
//             //ゲーム開始処理
//             if(Input.GetKeyDown(KeyCode.Space) && !flag){
//                 flag = true;
//                 SoundManager.Instance.PlayStart();
//                 StartCoroutine(Wait(0.8f,()=>{
//                     isPlaying = true;
//                     hideObject.SetActive(false);
//                 }));
//             }
//         }
//         //リトライ
//         if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("SampleScene");
//     }
//     private void CreateQuestion(){
//         // おそらく現在のレベルに応じた単語を引っ張ってきている。
//         // Linqで書き直せる
//         List<string> array = new List<string>();
//         if(nowLevel < 4){
//             array = level1;
//         }else if(nowLevel < 9){
//             array = level2;
//         }else if(nowLevel < 13){
//             array = level3;
//         }else{
//             array = level4;
//         }
//         //入力情報の初期化してる
//         index = 0;
//         inputText.text = "";
//         inputLog = "";
//         // 直近入力した単語がでないように
//         // ログ関連クラス分けたほうがいいのでは
//         string s = array[Random.Range(0, array.Count)];
//         while(questionLog.Contains(s)){
//             s = array[Random.Range(0, array.Count)];
//         }
//         questionLog.Add(s);
//         if(questionLog.Count > logMax) questionLog.RemoveAt(0);

//         question = s.ToCharArray();
//         questionText.text = new string(question);
//         guideText.text = questionText.text;
//         nowLevel++;
//         if(nowLevel >= 16) nowLevel = 0;
//         noMiss = true;
//     }

//     private void Correct(string key){
//         index++;
//         inputLog += key;
//         inputText.text = inputLog; 
//         correctCount++;
//         SoundManager.Instance.PlayCorrect();
//         if(index >= question.Length){
//             //score?
//             int sc = question.Length;
//             if(noMiss) sc = Mathf.CeilToInt(sc * 1.5f);
//             //エフェクトの管理別クラスにまとめれる
//             for(int i = 0; i < sc; i++){
//                 GameObject obj = Manager.Instance.CreatePrefab();
//                 obj.transform.position = new Vector3(Random.Range(-8f, 8f), 5, 0);
//                 obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
//                 StartCoroutine(Wait(15f, () => { Destroy(obj); }));
//             }
//             score += sc * 10;
//             SoundManager.Instance.PlayNext();
//             CreateQuestion();
//         }
//     }
//     private void Mistake(string key){
//         SoundManager.Instance.PlayMiss();
//         //ミスった文字だけ赤く染めてる
//         inputText.text = string.Format("{0}<color=#ff0000ff>{1}</color>", inputLog, key);
//         missCount++;
//         noMiss = false;
//     }

//     IEnumerator Wait(float time, UnityAction action){
//         yield return new WaitForSeconds(time);
//         action();
//     }
// }