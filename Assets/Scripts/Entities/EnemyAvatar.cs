using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : BaseAvatar
{
    private int score;
    // [SerializeField] GameObject objectText;
    [SerializeField] private GameObject explosionGO;
    void OnBecameInvisible(){
        Destroy(gameObject);
    }

    public override void Die() 
    {
        Instantiate(explosionGO, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // score +=100;
        // if (objectText != null){
        //     objectText.GetComponentInChildren<TextMesh>().text = "" + score + "";

        // }
    }

    public void GetGood(int k){
        if (k>5){
            MaxHealth+= 3;
        }
        if (k>10){
            GetComponent<AIBasicBulletGun>().damage+=1;
        }     
        if (k>50){
            MaxHealth+= 5;
        }
        if (k>100){
            MaxHealth+= 5;
        }            
        if (k>200){
            MaxHealth+= 10;
        }
        if (k>300){
            MaxHealth+= 25;
        }
    
    }
}
