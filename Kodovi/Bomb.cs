using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private ParticleSystem explodeParticleEffect;
    private Bomb bomb;

    private void Awake()
    {
        explodeParticleEffect = GetComponentInChildren<ParticleSystem>();
        bomb = FindObjectOfType<Bomb>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();

            // Event kad je bomba sliceana
            explodeParticleEffect.Play();
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Explode();

            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        // Funkcija ceka 0.7 sec da izbrise bomb game object
        yield return new WaitForSeconds(.7f);

        Destroy(bomb.gameObject);
    }
}
