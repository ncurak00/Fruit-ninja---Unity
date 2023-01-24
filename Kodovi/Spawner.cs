using System.Collections; //Kako bi koristiti IEnumerator vrstu funkcije
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs; //niz voća

    public GameObject bombPrefab;

    [Range(0f, 1f)] // u sucelju unitya, range varijabla postaje slider pa se moze manualno mijenjat
    public float bombChange = 0.1f;

    // vrijeme potrebno da se spawna voće
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    // kut pri kojem se voća mogu spawnat
    public float minAngle = -15f;
    public float maxAngle = 15f;

    // sila kojom se launchaju voća u nasu scenu
    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private void Awake() 
    {
        // Funkcija Awake koju Unity automatski poziva kada se ova funkcija inicijalizira 
        // Dohvaća box colider koji je spojen ovom skriptom
        spawnArea = GetComponent<Collider>(); //Neka spawnArea ode u komponente Spawnera i neka si pridijeli Box Collider komponentu
        bombChange = PlayerPrefs.GetFloat("bombChangeFloat");
    }

    private void OnEnable() 
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
 
    private IEnumerator Spawn()
    {
        // Ceka 2 sekunde prije nego krene spawnati voce
        yield return new WaitForSeconds(2f);

        while(enabled) 
        {
            // Randomizirani odabir voca za spawnanje
            GameObject prefab = fruitPrefabs[Random.Range(0,fruitPrefabs.Length)];

            if(Random.value < bombChange)
            {
                prefab = bombPrefab;
            }

            // Odabir pozicije spawnanja za pojedinacno voce
            Vector3 postion = new Vector3();
            postion.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            postion.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            postion.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Rotacija spawnera dijagonalno kako ne bi sva voca isla okomito
            Quaternion rotation = Quaternion.Euler(0f,0f, Random.Range(minAngle,maxAngle));

            // Spawn fruit
            GameObject fruit =  Instantiate(prefab, postion, rotation);

            // Unisti voce nakon sta prodje maxlifetime
            Destroy(fruit, maxLifetime);

            // Launchaj voce u igru
            float force = Random.Range(minForce, maxForce);
            // Dodaj vocu Unity komponentu Rigidbody koja sluzi za gravitaciju i dodaj joj funkciju force
            // koja ce launchat voce u igru
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
