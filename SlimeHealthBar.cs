using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlimeHealthBar : MonoBehaviour {

    public Image healthBarImage;
    public Enemy enemy;
    void Start() {
    }

    // Update is called once per frame
    public void UpdateHealthBar() {
        float duration = 0.75f * (enemy.health / enemy.maxHealth);
        healthBarImage.DOFillAmount( enemy.health / enemy.maxHealth, duration);
        Color newColor = Color.green;
        if ( enemy.health < enemy.maxHealth * 0.25f ) {
            newColor = Color.red;
        } else if ( enemy.health < enemy.maxHealth * 0.66f ) {
            newColor = new Color( 1f, .64f, 0f, 1f );
        }  healthBarImage.DOColor( newColor, duration );
    }
}
