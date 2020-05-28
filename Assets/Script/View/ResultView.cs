using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultView : MonoBehaviour, IView {
    public Const.View viewType => Const.View.Result;
    public Transform origin => transform;
    [SerializeField] private RectTransform title, score, other;
    [SerializeField] private Text scoreLabel, correctLabel, missLabel, rateLabel;
    [SerializeField] private Button backButton, tweetButton;
    [SerializeField] private string
        body = "ほんぶん",
        link = "https://unityroom.com",
        hashtags = "てきとー";

    private void Awake() {
        backButton.onClick.AddListener( () => {
            ViewManager.Open(Const.View.Title);
        });
        tweetButton.onClick.AddListener( Tweet );
    }

    public void Open() {
        Result result = Result.Load();
        correctLabel.text = $"{result.CorrectCount:000}";
        missLabel.text = $"{result.MissCount:000}";
        var rate = Mathf.RoundToInt(((float)result.CorrectCount / (result.CorrectCount + result.MissCount)) * 100);
        if(result.CorrectCount <= 0) rate = 0;
        rateLabel.text = $"{rate:000}";
        var displayScore = 0f;
        DOTween.Sequence()
            .AppendInterval(0.3f)
            .Append(title.DOAnchorPos(new Vector2(0, 180), 0.5f))
            .AppendInterval(0.2f)
            .Append(score.DOAnchorPos(new Vector2(0, -40), 0.3f))
            .Append(DOTween.To(
                () => displayScore,
                i => {
                    displayScore = i;
                    scoreLabel.text = $"{Mathf.CeilToInt(displayScore):000000}";
                },
                result.Score,
                1.3f
            ))
            .AppendInterval(0.2f)
            .Append(score.DOAnchorPos(Vector2.zero, 0.3f))
            .Join(other.DOAnchorPos(Vector2.zero, 0.5f))
            .OnComplete(() => {
                title.anchoredPosition = new Vector2(0, 180);
                score.anchoredPosition = Vector2.zero;
                other.anchoredPosition = Vector2.zero;

            }).Play();
    }

    public void Close() { }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) {
            DOTween.KillAll(true);
        }
    }

    private void Tweet() {
        var url = "https://twitter.com/intent/tweet?"
            + "text=" + body
            + "&url=" + link
            + "&hashtags=" + hashtags;
        #if UNITY_EDITOR
            Application.OpenURL(url);
        #elif UNITY_WEBGL
            // WebGLの場合は、ゲームプレイ画面と同じウィンドウでツイート画面が開かないよう、処理を変える
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
        #else
            Application.OpenURL(url);
        #endif
    }
}
