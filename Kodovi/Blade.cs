using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SaveScore;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    public TrailRenderer[] bladeTrails;
    private TrailRenderer bladeTrail;
    private bool slicing;

    public Data saveData;
    public int index;

    public Vector3 direction { get; private set; } // funkcija koja se moze mijenjati samo u ovoj klasi, ali je druge klase mogu citati
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.2f; // varijabla koja govori kolika je minimalna brzina potrebna za slice

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>(); // Neka bladeCollider dobije komponentu Collidera
        bladeTrail = GetComponentInChildren<TrailRenderer>(); // Neka dijete glavnog bojekta dobije komponentu TrailRenderera

        // Load index from json saved from color change menu
        // public int index 
        saveData = SaveScore.LoadMyData();
        index = saveData.index;

        // index
        bladeTrail.colorGradient = bladeTrails[index].colorGradient;
        //bladeTrail.startColor = bladeTrails[index].startColor;
        //bladeTrail.endColor = bladeTrails[index].endColor;
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    // Unity funkcija koja se poziva svakog framea u igrici koja provjerava je li pritisnut mis
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        // Updateaj poziciju collidera po poziciji misa
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f; // Igra je 2D, stoga Z os treba imati poziciju 0f

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        // Updateaj poziciju collidera po poziciji misa
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        // Govori nam koliko se brzo krece -> koliko se pomaklo u zadnjem frameu (distance/time)
        float velocity = direction.magnitude / Time.deltaTime;
        // AKo je brzina veca od minimalne brzine potrebne za slice voca collider se aktivira
        // Sprjecava akritivarnje collidera bez pomicanja misa
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

    }
}
