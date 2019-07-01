﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimSpeed : MonoBehaviour{
    private Animator anim;
    [SerializeField, Range(0,5)] private float animSpeed;
    void Start(){
        anim = GetComponent<Animator>();
        SetSpeed(animSpeed);
    }
    public void SetSpeed(float speed){
        anim.speed = speed;
    }
}
