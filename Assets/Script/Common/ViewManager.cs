using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewManager : SingletonMonoBehaviour<ViewManager>{
    [SerializeField] private GameObject
        titleView,
        optionView,
        gameView,
        resultView;
    private IView currentView, lastView;

    public static void Open(Const.View viewType, bool close = true) {
        if(Instance.currentView != null) Instance.lastView = Instance.currentView;
        if(close && Instance.lastView != null) Close(Instance.lastView.viewType);
        Instance.currentView = GetView(viewType);
        Instance.currentView.origin.gameObject.SetActive(true);
        Instance.currentView.Open();
    }

    public static void Close(Const.View viewType) {
        var view = GetView(viewType);
        if(view == Instance.currentView) Instance.currentView = Instance.lastView;
        view.origin.gameObject.SetActive(false);
        print(view.origin.name);
        print(view.origin.gameObject.activeSelf);
        view.Close();
    }

    private static IView GetView(Const.View viewType) {
        GameObject view;
        switch(viewType) {
            case Const.View.Game: view = Instance.gameView; break;
            case Const.View.Result: view = Instance.resultView; break;
            case Const.View.Option: view = Instance.optionView; break;
            default: view = Instance.titleView; break;
        }
        return view.GetComponent<IView>();
    }
}