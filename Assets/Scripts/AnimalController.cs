using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    [HideInInspector] public Food foodPreference;
    [HideInInspector] public int fullness = 0;

    [Header("Customization")]
    public int hungerLevel = 1;
    public int pointValue = 100;

    [Header("UI")]
    public Slider healthSlider;
    public Image foodImage;
    public Image backgroundImage;
    private Color happyColor = new Color(0.26f, 0.925f, 0.0f, 1.0f);
    private Color angryColor = new Color(0.925f, 0.0392f, 0.0f, 1.0f);

    bool inPlay = false;
    Renderer animalRenderer;
    Image fillImage;

    public int Fullness
    {
        get
        {
            return fullness;
        }
        set
        {
            fullness = value;
            healthSlider.value = fullness;
            if(fullness >= hungerLevel)
            {
                FindObjectOfType<PlayerController>()?.AddPoints(pointValue);
                backgroundImage.color = happyColor;
                Destroy(transform.root.gameObject, 0.1f);
            }
        }
    }

    void Start()
    {
        animalRenderer = GetComponent<SkinnedMeshRenderer>();
        fillImage = healthSlider.fillRect.GetComponent<Image>();

        foodImage.transform.rotation = Quaternion.Euler(90, 0, 0);
        healthSlider.maxValue = hungerLevel;
        fillImage.color = happyColor;
        Fullness = 0;
    }

    void LateUpdate()
    {
        if (!animalRenderer.isVisible && inPlay)
        {
            FindObjectOfType<PlayerController>()?.LoseHealth();
            Destroy(transform.root.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if(player)
        {
            player.LoseHealth();
            Destroy(transform.root.gameObject);
        }

        FoodController food = other.GetComponent<FoodController>();
        if(food && inPlay)
        {
            FeedAnimal(food);
        }
    }

    void FeedAnimal(FoodController foodController)
    {
        Destroy(foodController.gameObject);

        if(foodController.food.Equals(foodPreference))
        {
            fillImage.color = happyColor;
            Fullness++;
        }
        else if(Fullness > 0)
        {
            fillImage.color = angryColor;
            Fullness--;
        }
    }

    public void SetFoodPreference(Food food)
    {
        foodPreference = food;
        foodImage.sprite = food.foodSprite;
    }

    private void OnBecameVisible()
    {
        inPlay = true;
    }

}
