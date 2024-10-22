using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet
{
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        UpdatePosition();
    }
    



}
