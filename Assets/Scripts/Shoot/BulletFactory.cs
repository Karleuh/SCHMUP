using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletListStack;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory instance = null;
    public static BulletFactory Instance => instance;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int initialBulletCount;

    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        for (int i =0; i < initialBulletCount; i++)  {
            Bullet _bullet = Instantiate(bulletPrefab, bulletPrefab.transform.position, bulletPrefab.transform.rotation).GetComponent<Bullet>();
            _bullet.setActiveRenderer(false);
            _bullet.enabled = false;
            
            bulletList.Push(_bullet);

        } 

    }

    // Get component of bullet of type . or instantiate new bullet. Returns component : only bulletscript.init(...) et ggwp
    static public Bullet GetBullet(BulletType type){
        
        Bullet _bullet;
        if (bulletList.TryPop(out _bullet)) { // Si la liste a des éléments
            //Bullet potentiellement déjà utilisée : on ractive son renderer et script
            
            _bullet.enabled = true;
            _bullet.setActiveRenderer(true);

        }   
        else {
            _bullet = Instantiate(Instance.bulletPrefab).GetComponent<Bullet>();
        }

        if (_bullet != null) {
        _bullet.type = type;
        _bullet.updateTargetLayerAndLocalScale();
        return _bullet;
        }
        else {
            return null;
        }
    }

    static public void Release(Bullet _bullet) {
        //Désactive le renderer puis le component, deviens available, reset l'angle donc rejoins la liste.
        if (_bullet != null) { 
            _bullet.SendOutOfScreen();
            bulletList.Push(_bullet);
            _bullet.setActiveRenderer(false);
            _bullet.Reset();
            _bullet.enabled = false;
            
        }

    }

}
