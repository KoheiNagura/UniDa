using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Question {
    public string Text { get; private set; }
    public int Level { get; private set; }
    public static List<Question> All;

    public Question(string word) {
        Text = word;
        Level = (word.Length - 1) / 4 + 1;
    }

    public static void Initialize() {
        All = new List<Question>();
        var csv = (Resources.Load("data") as TextAsset).text;
        var data = csv.Split(',');
        All = data.Select( i => new Question(i) ).ToList();
    }

    public static List<Question> GetByLevel(int level){
        return All.Where( i => i.Level == level ).ToList();
    }

    public char GetChar(int index) => Text[index];
}