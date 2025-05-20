using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam; // Fa referència a la càmera del jugador FPS
    public float range = 30f; // Fins on volem que arribin els tirs

    public float damage = 25f;

    public Animator playerAnimator;
    
    public ParticleSystem FlashParticleSystem;
    public GameObject BloodParticleSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }


    }

    public void Shoot()
    {
        playerAnimator.SetBool("isShooting", true);
        FlashParticleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {
            //Debug.Log("Tocat!");
            // Si no hem ferit a un Zombie, la component EnemyManager valdrà null, però sinò prendrà el valor de la component del Zombie que hem ferit.
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                GameObject particleInstance = Instantiate(BloodParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
                particleInstance.transform.parent = hit.transform;
                particleInstance.GetComponent<ParticleSystem>().Play();
                enemyManager.Hit(damage);
            }
        }

    }
}
