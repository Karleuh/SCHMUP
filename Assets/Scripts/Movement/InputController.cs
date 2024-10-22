using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour
{

    private Vector2 normalizedDirection;
    private Engines playerEngines;
    private BulletGun playerBulletGun;
    // Start is called before the first frame update
    void Start()
    {
        playerEngines = GetComponent<Engines>();
        playerBulletGun = GetComponent<BulletGun>();
    }

    // Update is called once per frame
    void Update()
    {

        ///// MOVEMENT

        playerEngines.NormalizedDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;


        ///// SHOOT
        if (Input.GetKey(KeyCode.Space) && !playerBulletGun.rechargeForcee){
            playerBulletGun.Fire();
        }
        else{
            playerBulletGun.Recharge();
        }

        /// Reset shoot variables if space is up
        if (Input.GetKeyUp(KeyCode.Space)) {
            playerBulletGun.Reset();
        }


        ///// Change Gun
        if (Input.GetKeyDown(KeyCode.Tab)) {

            playerBulletGun.type = (FireType) ((int) (playerBulletGun.type +1) % Enum.GetValues(typeof(FireType)).Length);

            playerBulletGun.UpdateCooldowns();
            playerBulletGun.Reset();

        }
        ///// 
    }
}
