using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour {

    public Image healthBarImage;
    private Player player;
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    public void UpdateHealthBar() {
        float duration = 0.75f * (player.health / player.maxHealth);
        healthBarImage.DOFillAmount( player.health / player.maxHealth, duration);
        Color newColor = Color.green;
        if ( player.health < player.maxHealth * 0.25f ) {
            newColor = Color.red;
        } else if ( player.health < player.maxHealth * 0.66f ) {
            newColor = new Color( 1f, .64f, 0f, 1f );
        }  healthBarImage.DOColor( newColor, duration );
    }
}
