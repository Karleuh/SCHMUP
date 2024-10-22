using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerAvatar : BaseAvatar
{   
    public GameObject canvas;
    private SlidersManager slidersManager;

    [SerializeField] GameManager gameManager;

    void Start() {
        canvas = GameObject.Find("Canvas");
        slidersManager = canvas.GetComponent<SlidersManager>();
    }
    public override void Die(){
        
        // RETOUR LOBBY NEUIL
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        slidersManager.updateHealthSlider(Health, MaxHealth);
    }


}
