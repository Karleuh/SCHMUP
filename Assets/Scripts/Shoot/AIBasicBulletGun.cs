using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : MonoBehaviour
{

    [SerializeField] private Vector3 doubleFireOffset;
    [SerializeField] private float circleShotOffset;

    private float angle =0;
    [SerializeField]
    public float damage;

    [SerializeField]
    private Vector2 speed;

    [SerializeField]
    private float maxCoolDown;

    [SerializeField]
    private float doubleStraightCoolDown;
    [SerializeField]
    private float circleCoolDown;

    [SerializeField]
    private float tripleCoolDown; 

    [SerializeField]
    private Transform bulletGunTransform;
    public gunType bulletGunType;
    public enum gunType {
        Straight,
        DoubleStraight,
        Circle,
        Triple,
    }
    public void Fire(){
        {
            Bullet _bullet;
            _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
            _bullet.Init(damage, speed, BulletType.EnemyBullet, bulletGunTransform.position);
        }
    }

    protected void  FireTwiceStraight() {

        Bullet _bullet;
        // Direct hors du MG
        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speed, BulletType.EnemyBullet, bulletGunTransform.position + doubleFireOffset/2);
        // Décalé par un offset en y.
        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speed, BulletType.EnemyBullet, bulletGunTransform.position - doubleFireOffset/2);
    }

    protected void  FireTriceDiags() {
        Bullet _bullet;
        //straight
        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speed, BulletType.EnemyBullet, bulletGunTransform.position);

        //Diag 45°
        Vector2 speedBis = new Vector2(Mathf.Sqrt(2)/2, Mathf.Sqrt(2)/2);
        speedBis *= speed.magnitude;

        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speedBis, BulletType.EnemyBullet, bulletGunTransform.position, 45f);

        //Diag -45°
        speedBis = new Vector2(Mathf.Sqrt(2)/2, -Mathf.Sqrt(2)/2);
        speedBis *= speed.magnitude;

        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speedBis, BulletType.EnemyBullet, bulletGunTransform.position, -45f);

    }

    protected void  FireInCircle() {
        Bullet _bullet;
        angle +=   circleShotOffset;
        //abs(cos(angle)) -> on tire pas derrière nous abuse qd même
        Vector2 speedBis = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*-speed.magnitude/2;

        _bullet = BulletFactory.GetBullet(BulletType.EnemyBullet);
        _bullet.Init(damage,speedBis, BulletType.EnemyBullet, bulletGunTransform.position, 57*angle);
    }


    // Start is called before the first frame update
    void Start()
    {
        switch (bulletGunType){
            case gunType.DoubleStraight :
                InvokeRepeating("FireTwiceStraight", 0.5f, doubleStraightCoolDown);
                break;
            case gunType.Circle :
                InvokeRepeating("FireInCircle", 0.5f, circleCoolDown);
                break;
            case gunType.Triple :
                InvokeRepeating("FireTriceDiags", 0.5f, tripleCoolDown);
                break;
            default :
                InvokeRepeating("Fire", 0.5f, maxCoolDown);
                break;
        }
    }
}
