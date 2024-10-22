using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType
    {
        PlayerBullet,
        EnemyBullet,
    }

public abstract class Bullet : MonoBehaviour
{

    [SerializeField] Vector3 outOfScreen;

    [SerializeField]
    private LayerMask mobLayer;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Vector2 speed;

    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private LayerMask targetLayer;

    public float Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    public Vector2 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Vector2 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }


    public LayerMask TargetLayer
    {
        get { return targetLayer; }
        set { targetLayer = value; }
    }

    private float angle;


    public BulletType type;

    public virtual void Init(float damage, Vector2 speed, BulletType type, Vector3 position, float angle =0){
        this.damage = damage;
        this.speed= speed;
        this.position = position;


        this.type = type;

        this.angle = angle;

        transform.Rotate(0f,0f,angle, Space.Self);
    }

    public virtual void UpdatePosition(){
        position += speed*Time.deltaTime;
        transform.position = position;
    }

    // Détruite quand invisible
    void OnBecameInvisible(){
        if (this!=null){
            BulletFactory.Release(this);
        }

    }

    // Collisions
    private void OnTriggerEnter2D(Collider2D col) {
        
        if (((1<<col.gameObject.layer) & targetLayer) != 0) {
            col.gameObject.GetComponent<BaseAvatar>().TakeDamage(damage);
            // BulletFactory.Release(this);
            setActiveRenderer(false);
        }

    }

    public void updateTargetLayerAndLocalScale()
    {        

        if (type == BulletType.PlayerBullet){
            TargetLayer = mobLayer;
        }

        if (type == BulletType.EnemyBullet){
            TargetLayer = playerLayer;
            transform.localScale *=-1;

        }

    }

    public void setActiveRenderer(bool a){
        gameObject.GetComponent<Renderer>().enabled = a;
    }


    public void Reset(){
        
        if(type == BulletType.EnemyBullet){
            transform.localScale *= -1;
        }

        transform.Rotate(0f,0f, -angle, Space.Self);
    }

    public void SendOutOfScreen(){
        transform.position = outOfScreen;
    }
}
