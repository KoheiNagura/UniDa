using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour {
    [SerializeField] private Text questionLabel, guideLabel, inputLabel;
    private string inputCache;

    public void SetQuestion(Question question) {
        questionLabel.text = question.Text;
        guideLabel.text = question.Text;
        inputLabel.text = "";
        inputCache = "";
    }
    
    public void SetInput(string key, bool isMiss) {
        if(isMiss) {
            inputLabel.text = inputCache + $"<color=#ff0000ff>{key}</color>";
        }else inputLabel.text = inputCache += key; 
    }
}