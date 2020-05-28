using UnityEngine;

interface IView {
    Const.View viewType { get; }
    Transform origin { get; }
    void Open();
    void Close();
}