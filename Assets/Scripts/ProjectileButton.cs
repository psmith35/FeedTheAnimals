using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProjectileButton : MonoBehaviour
{
    private PlayerController player;
    private Button fireButton;

    public GameObject projectile;
    public Image buttonImage;
    public string fireInput = "Fire1";

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        fireButton = GetComponent<Button>();

        fireButton.onClick.AddListener(FireProjectile);
        FoodController foodController = projectile.GetComponent<FoodController>();
        if(foodController)
        {
            buttonImage.sprite = foodController.food.foodSprite;
        }
        else
        {
            buttonImage.sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fireInput))
        {
            fireButton.onClick.Invoke();
        }
    }

    public void FireProjectile()
    {
        if (projectile && player)
        {
            Instantiate(projectile, player.FiringPosition, projectile.transform.rotation);
        }
    }
}
