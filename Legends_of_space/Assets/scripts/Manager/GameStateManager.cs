﻿
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;

   
    void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

