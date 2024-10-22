using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletFactory;

public enum FireType {
    DoubleStraight,
    TripleDiag,
    SuperCircle,
}




public class BulletGun : BaseBulletGun
{

    

    [SerializeField]
    private float superCircleCooldown;
    [SerializeField]
    private float regularCooldown;


    [SerializeField]
    private float FireInCircleEnergyCost;

    [SerializeField]
    private float FireTriceDiagsEnergyCost;

    [SerializeField]
    private float FireTwiceStraightEnergyCost;

    [SerializeField]
    private float energyRechargeSpeed;
    public float energy;

    [SerializeField] private float energyMax;
    public bool rechargeForcee;

    [SerializeField]
    public FireType type;

    [SerializeField]
    private float damage;
    
    [SerializeField]
    private Vector2 speed;
    
    [SerializeField]
    private float cooldown;

    [SerializeField]

    private Vector3 doubleFireOffset;

    [SerializeField]
    private float circleShotOffset;

    [SerializeField]
    private float angle;

    public float Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    public Vector2 Speed
    {
        get { return speed; }
        private set { speed = value; }
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }


    private float cd;


    [SerializeField]
    private GameObject bullet;
    private Bullet _bullet;
    private bool canShoot = true;

    [SerializeField]

    private Transform bulletGunTransform;

    public GameObject canvas;
    private SlidersManager slidersManager;
    void Start() {
        canvas = GameObject.Find("Canvas");
        slidersManager = canvas.GetComponent<SlidersManager>();
    }

    public void Fire(){
        if (canShoot && energy > 0) {
            switch (type) {

                case FireType.DoubleStraight:
                    FireTwiceStraight();
                    break;
                case FireType.TripleDiag:
                    FireTriceDiags();
                    break;
                case FireType.SuperCircle:
                    FireInCircle();
                    break;
                default:
                    break;
                }
            //Juste après avoir tiré
            if (energy <= 0) {
                rechargeForcee = true;
            }    
            canShoot = false;
            StartCoroutine(WaitForCooldownCanShoot(Cooldown));

        }

        if (energy == energyMax) {
            rechargeForcee = false;
        }

        }

    void FireTwiceStraight() {


        // Energy
        energy -= FireTwiceStraightEnergyCost;
        slidersManager.updateEnergySlider(energy, energyMax);

        // Direct hors du MG
        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speed, BulletType.PlayerBullet, bulletGunTransform.position + doubleFireOffset/2);
        // Décalé par un offset en y.
        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speed, BulletType.PlayerBullet, bulletGunTransform.position - doubleFireOffset/2);
    }

    void FireTriceDiags() {


        //Energy 
        energy -= FireTriceDiagsEnergyCost;
        slidersManager.updateEnergySlider(energy, energyMax);
        
        //straight
        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speed, BulletType.PlayerBullet, bulletGunTransform.position);

        //Diag 45°
        Vector2 speedBis = new Vector2(Mathf.Sqrt(2)/2, Mathf.Sqrt(2)/2);
        speedBis *= speed.magnitude;

        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speedBis, BulletType.PlayerBullet, bulletGunTransform.position, 45f);

        //Diag -45°
        speedBis = new Vector2(Mathf.Sqrt(2)/2, -Mathf.Sqrt(2)/2);
        speedBis *= speed.magnitude;

        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speedBis, BulletType.PlayerBullet, bulletGunTransform.position, -45f);

    }

    void FireInCircle() {

        //Energy
        energy -= FireInCircleEnergyCost;
        slidersManager.updateEnergySlider(energy, energyMax);


        angle += circleShotOffset;
        //abs(cos(angle)) -> on tire pas derrière nous abuse qd même
        Vector2 speedBis = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*speed.magnitude/2;

        _bullet = BulletFactory.GetBullet(BulletType.PlayerBullet);
        _bullet.Init(damage,speedBis, BulletType.PlayerBullet, bulletGunTransform.position, 57*angle);
    }


    public void Recharge()
    {
        if (energy<100) {
            energy += energyRechargeSpeed*Time.deltaTime;
            slidersManager.updateEnergySlider(energy, energyMax);
        }
        else {
            energy = 100;
            rechargeForcee = false;
        }
    }

    public IEnumerator WaitForCooldownCanShoot(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    public void UpdateCooldowns(){
        switch (type) {
            case FireType.SuperCircle:
                Cooldown = superCircleCooldown; 
                break;
            default : 
                Cooldown = regularCooldown;
                break;
        }
    }

    public void Reset(){
        angle = 0;
    }

}
