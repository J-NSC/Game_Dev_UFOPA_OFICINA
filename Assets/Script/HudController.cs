using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public static HudController inst;
    
    [SerializeField] TMP_Text scoreTxt;

    public int score = 0;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }


    void Update()
    {
        scoreTxt.text = score.ToString();
    }

    
}
