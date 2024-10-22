using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ExampleClass : MonoBehaviour {
    void Start() {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var ts = ps.textureSheetAnimation;
            ts.enabled = true;
        
        ts.rowMode = ParticleSystemAnimationRowMode.Custom;
        ts.rowIndex = 3;
    }
}
