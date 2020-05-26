using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Linq;
using DG.Tweening;

public class GameView : MonoBehaviour, IView {
    public Const.View viewType => Const.View.Game;
    public Transform origin => transform;
    [SerializeField] private Text scoreLabel, timerLabel;
    [SerializeField] private QuestionView questionView;
    [SerializeField] private AudioClip start, correct, miss;
    [SerializeField] private GameObject maskObject;
    private Result result;
    private Keyboard keyboard;
    private Question question;
    private QuestionLog questionLog;
    private bool 
        isPlaying = false,
        isInteractive = false,
        noMiss = false;
    private int 
        nowIndex = 0,
        nowLevel = 0,
        maxLevel = 16;
    private float 
        timer = 60f,
        maxTimer = 60f;

    private void Awake() {
        if(result == null) result = new Result();
        else result.Reset();
        keyboard = gameObject.AddComponent<Keyboard>();
        questionLog = new QuestionLog(5);
        keyboard.OnInput = key => JudgeKey(key);
    }

    private void Update() {
        // 開始処理
        if(!isPlaying && isInteractive) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                isInteractive = false;
                AudioManager.PlayOneShot(start);
                StartCoroutine(DelayMethod( .8f, GameStart ));
            }
        }
        // リトライ
        if(isInteractive) {
            if(Input.GetKeyDown(KeyCode.Escape)) Reset();
        }
    }

    public void Open() => Reset();

    public void Close() {
        StopAllCoroutines();
        EffectManager.Hide();
    }

    private void Reset() {
        DOTween.KillAll();
        nowLevel = 1;
        timer = maxTimer;
        isPlaying = false;
        isInteractive = true;
        scoreLabel.text = $"SCORE\n------";
        timerLabel.text = $"TIME\n--";
        maskObject.SetActive(true);
        questionLog.Clear();
        SelectQuestion();
    }

    private void GameStart() {
        isInteractive = true;
        isPlaying = true;
        maskObject.SetActive(false);
        UpdateScoreLabel(0);
        UpdateTimerLabel(60f);
        DOTween.To(
            () => timer,
            value => UpdateTimerLabel(value),
            0,
            maxTimer
        ).SetEase(Ease.Linear)
        .OnComplete( () => {
            UpdateTimerLabel(0f);
        });
        StartCoroutine(DelayMethod( maxTimer, TimeUp ));
    }

    private void SelectQuestion() {
        var list = Question.GetByLevel( (nowLevel / 4) + 1 );
        list = list.Where( i => !questionLog.Contains(i) ).ToList();
        question = list.OrderBy( i => System.Guid.NewGuid() ).First();
        questionLog.Add(question);
        questionView.SetQuestion(question);
        nowIndex = 0;
        nowLevel++;
        noMiss = true;
        if(nowLevel > maxLevel) nowLevel = 1;
    }

    public void UpdateScoreLabel(int score) {
        scoreLabel.text = $"SCORE\n{score:00000}";
    }
    
    public void UpdateTimerLabel(float timer) {
        timer = Mathf.FloorToInt(timer);
        timerLabel.text = $"TIME\n{timer:00}";
    }

    private void TimeUp() {
        isPlaying = false;
        isInteractive = false;
        DOTween.KillAll(true);
        Result.Save(result);
        ViewManager.Open(Const.View.Result);
    }
    
    private void JudgeKey(string key) {
        if(!isPlaying) return;
        var isMiss = (key[0] != question.GetChar(nowIndex));
        questionView.SetInput(key, isMiss);
        if(isMiss) Misstake();
        else Correct();
    }

    private void Correct() {
        nowIndex++;
        result.SetCorrect(result.CorrectCount + 1);
        AudioManager.PlayOneShot();
        if(question.Text.Length > nowIndex) return;
        var score = question.Text.Length * (noMiss ? 1.5f : 1);
        int startScore = result.Score;
        int endScore = startScore + Mathf.CeilToInt(score);
        DOTween.To(
            () => startScore,
            value => UpdateScoreLabel(value),
            endScore,
            .5f
        ).OnComplete(() => {
            UpdateScoreLabel(endScore);
        });
        result.SetScore(endScore);
        for(int i = 0; i < score; i++){
            GameObject effect = EffectManager.GetRandomEffect();
            effect.transform.position = new Vector3(Random.Range(-8f, 8f), 5, 0);
            effect.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            StartCoroutine(DelayMethod( 15f, () => effect.SetActive(false) ));
        }
        SelectQuestion();
    }

    private void Misstake() {
        AudioManager.PlayOneShot(miss);
        result.SetMiss(result.MissCount + 1);
        noMiss = false;
    }

    private IEnumerator DelayMethod(float sec, UnityAction action){
        yield return new WaitForSeconds(sec);
        action();
    }
}