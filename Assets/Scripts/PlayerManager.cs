using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;
    public GameManager gameManager;

    // Variable per a poder controlar la càmera
    public GameObject playerCamera;
    
    // Variable per controlar el temps de vibració de la càmera
    public float shakeTime = 1f;
    public float shakeDuration = 0.125f;
    private Quaternion playerCameraOriginalRotation;
    public CanvasGroup hitPanel;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            CameraShake();
        }else if(playerCamera.transform.localRotation != playerCameraOriginalRotation)
        {
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }
        if (hitPanel.alpha > 0)
        {
            hitPanel.alpha -= Time.deltaTime;
        }


        
    }

    public void Hit(float damage)
    {
        hitPanel.alpha = 0.7f;
        health -= damage;
        gameManager.HpBar.fillAmount = health / maxHealth;
        shakeTime = 0f;
        CameraShake();
        if (health <= 0)
        {
            gameManager.GameOver();
        }
        
    }
    
    public void CameraShake()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2f, 2f), 0, 0);
    }

}
