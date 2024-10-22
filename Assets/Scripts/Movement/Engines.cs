using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour
{

    [SerializeField]
    private Vector2 position;
    public Vector2 Position {
        get {
        return transform.position;
            }
        private set {
            transform.position = value;
            }
    }

    [SerializeField]

    // Setter public modifié dans scripts controller
    private Vector2 normalizedDirection;
    public Vector2 NormalizedDirection {
        get {
        return this.normalizedDirection;
            }
        set {
            this.normalizedDirection = value;
            }
    }

    private BaseAvatar avatar;


    void Start() 
    {
        avatar = GetComponent<BaseAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        Position += normalizedDirection*avatar.MaxSpeed*Time.deltaTime;
    }
}
