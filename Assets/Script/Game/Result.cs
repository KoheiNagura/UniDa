using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Result {
    [SerializeField] private int _score, _correct, _miss;
    public int Score => _score;
    public int CorrectCount => _correct;
    public int MissCount => _miss;

    public void Reset() {
        SetScore(0);
        SetCorrect(0);
        SetMiss(0);
    }
 
    public void SetScore(int score) => _score = score;

    public void SetCorrect(int count) => _correct = count;

    public void SetMiss(int count) => _miss = count;

    public static void Save(Result result) {
        var json = JsonUtility.ToJson(result);
        PlayerPrefs.SetString(Const.SaveKeys.LastResult, json);
    }

    public static Result Load() {
        Result result;
        if(PlayerPrefs.HasKey(Const.SaveKeys.LastResult)) {
            result = JsonUtility.FromJson<Result>(
                PlayerPrefs.GetString(Const.SaveKeys.LastResult)
            );
        }else result = new Result();
        return result;
    }

}