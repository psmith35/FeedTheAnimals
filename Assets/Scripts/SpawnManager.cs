using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum ScreenSide { Left, Right, Top, Bottom }

    [Header("Animals")]
    public GameObject[] animalPrefabs;
    public Food[] foodPreferences;

    [Header("Spawning")]
    public float startDelay = 2.0f;
    public float spawnInterval = 1.5f;
    public ScreenSide[] screenSides;

    private Vector3 screenBounds;

    // Update is called once per frame
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));
        InvokeRepeating("SpawnAnimal", startDelay, spawnInterval);
        if(animalPrefabs.Length == 0 || foodPreferences.Length == 0)
        {
            throw new System.Exception("Problem: Must add animals and food to SpawnManager.");
        }
    }

    void SpawnAnimal()
    {
        if(screenSides.Length != 0)
        {
            ScreenSide side = screenSides[Random.Range(0, screenSides.Length)];
            SpawnRandomAnimal(side);
        }
    }

    void SpawnRandomAnimal(ScreenSide side)
    {
        GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];       

        Vector3 animalSize = Vector3.Scale(animalPrefab.transform.GetComponentInChildren<BoxCollider>().size, animalPrefab.transform.localScale);
        float spawnX = animalSize.x * 2;
        float spawnZ = animalSize.z * 2;

        Vector3 position = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        switch(side)
        {
            case ScreenSide.Left:
                position = new Vector3(-screenBounds.x - spawnZ, 0, Random.Range(-screenBounds.z + spawnX, screenBounds.z - spawnX));
                targetPosition = Vector3.Reflect(position, Vector3.right);
                rotation = Quaternion.Euler(0, 90, 0);
                break;
            case ScreenSide.Right:
                position = new Vector3(screenBounds.x + spawnZ, 0, Random.Range(-screenBounds.z + spawnX, screenBounds.z - spawnX));
                targetPosition = Vector3.Reflect(position, Vector3.left);
                rotation = Quaternion.Euler(0, -90, 0);
                break;
            case ScreenSide.Top:
                position = new Vector3(Random.Range(-screenBounds.x + spawnX, screenBounds.x - spawnX), 0, screenBounds.z + spawnZ);
                targetPosition = Vector3.Reflect(position, Vector3.forward);
                rotation = Quaternion.Euler(0, 180, 0);
                break;
            case ScreenSide.Bottom:
                position = new Vector3(Random.Range(-screenBounds.x + spawnX, screenBounds.x - spawnX), 0, -screenBounds.z - spawnZ);
                targetPosition = Vector3.Reflect(position, Vector3.back);
                rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        GameObject animal = Instantiate(animalPrefab, position, rotation);

        AnimalController animalController = animal.GetComponentInChildren<AnimalController>();
        if (animalController)
        {
            animalController.SetFoodPreference(foodPreferences[Random.Range(0, foodPreferences.Length)]);
            animalController.targetPosition = targetPosition;
        }

    }

}