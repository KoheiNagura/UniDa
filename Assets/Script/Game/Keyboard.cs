using UnityEngine;
using UnityEngine.Events;

public class Keyboard : MonoBehaviour{
    private string[] keys =  {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z","1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
    private bool isShift;
    public UnityAction<string> OnInput;

    private void Update() {
        isShift = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        foreach(var key in keys) {
            if(Input.GetKeyDown(key)) {
                OnInput?.Invoke( isShift ? key.ToUpper() : key );
            }
        }
    }
}