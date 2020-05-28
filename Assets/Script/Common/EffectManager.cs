using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EffectManager : SingletonMonoBehaviour<EffectManager> {
    [SerializeField] private GameObject[] prefabs;
    private List< (int id, GameObject effect) > pool;

    public static void Initialize() {
        Instance.pool = new List<(int id, GameObject effect)>();
        //pool用に各エフェクト10個生成して保管
        for(var i = 0; i < Instance.prefabs.Length; i ++) {
            Instance.pool.AddRange(
                Enumerable.Range(0, 10)
                .Select( x => ( i, InstantiateAndHide( Instance.prefabs[i] )) )
                .ToList()
            );
        }
    }

    public static GameObject GetEffect(int id){
        var effects = Instance.pool.Where( i => i.id == id )
            .Where( i => !i.effect.activeSelf ).ToList();
        GameObject effect;
        if(effects.Count > 0) effect = effects.First().effect;
        else effect = Instantiate( Instance.prefabs[id] );
        effect.SetActive(true);
        return effect;
    }

    public static GameObject GetRandomEffect() {
        return GetEffect( Random.Range(0, Instance.prefabs.Length) );
    }

    public static void Hide() {
        var effects = Instance.pool.Select( i => i.effect ).Where( i => i.activeSelf );
        foreach(var effect in effects) effect.SetActive(false);
    }
    
    private static GameObject InstantiateAndHide(GameObject prefab) {
        var obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }
}