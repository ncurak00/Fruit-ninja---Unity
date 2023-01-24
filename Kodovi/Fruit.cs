using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody; //koristimo da sliced fruit dobije svoju vlastitu gravitaciju nakon sto ga prerezmo
    private Collider fruitCollider; //iskljucimo ga kada prerezemo voce, kako se isti komad ne bi rezao vise puta
    private ParticleSystem juiceParticleEffect; //efekt nakon sta se prereze voce

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 postion, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(); //poziv funkcije increasescore u kojoj se poziva brojac++

        whole.SetActive(false); 
        sliced.SetActive(true);

        fruitCollider.enabled = false; //gasimo collison nakon sta prerezemo voce
        juiceParticleEffect.Play(); //pokreni particle effect

        // Unity funkcija Mathf.Atan2 koja daje rjesenje u radijanima stoga je pretvaramo u stupnjeve
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Niz slices objekata neka pridobije komponentu Rigidbody koja sluzi za gravitaciju
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        //Postavljanje brzine, smjera i sile za svaki od objekata iz niza
        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, postion, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) //nas blade je postavljen na Player tag, pa tako usporedjujemo playera sa ostalim tagovima
        {
            Blade blade = other.GetComponent<Blade>(); //varijabla blade tipa Blade iz druge skripte
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
