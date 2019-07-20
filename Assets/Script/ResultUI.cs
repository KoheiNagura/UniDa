﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultUI : UIBase{
    [SerializeField] private RectTransform title, score, other;
    [SerializeField] private Text scoreText, correctText, missText, rateText;
    void Start(){
        correctText.text = Main.correctCount.ToString("D3");
        missText.text = Main.missCount.ToString("D3");
        rateText.text = Mathf.RoundToInt(((float)Main.correctCount / (Main.correctCount + Main.missCount)) * 100).ToString("D3");
        float displayScore = 0;
        DOTween.Sequence()
            .AppendInterval(0.3f)
            .Append(title.DOAnchorPos(new Vector2(0, 180), 0.5f))
            .AppendInterval(0.2f)
            .Append(score.DOAnchorPos(new Vector2(0, -40), 0.3f))
            .Append(DOTween.To(
                () => displayScore,
                i => {
                    displayScore = i;
                    scoreText.text = Mathf.CeilToInt(displayScore).ToString("D6");
                },
                Main.score,
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
    void Update(){
        if(Input.anyKeyDown) DOTween.KillAll(true);
    }
}