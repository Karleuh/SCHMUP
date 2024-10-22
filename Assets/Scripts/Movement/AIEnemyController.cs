using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    private Engines enemyEngine;
    void Start()
    {
        enemyEngine = GetComponent<Engines>();
        enemyEngine.NormalizedDirection = Vector2.left;
    }

}
