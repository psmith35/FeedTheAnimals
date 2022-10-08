using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Control")]
    public float speed = 10.0f;
    public float firingOffset = 1.0f;

    [Header("UI")]
    public Text scoreText;
    public Image[] healthImages;
    public Color fullColor = new Color(1.0f, 0.0f, 0.0f);
    public Color blankColor = new Color(0.5f, 0.0f, 0.0f);

    [Header("EndGame")]
    public GameObject endPanel;
    public Text finalScoreText;

    float widthRange;
    float heightRange;

    int playerHealth;
    int playerScore;

    public int Health
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = Mathf.Min(value, int.MaxValue);
            for(int i = 0; i < healthImages.Length; i++)
            {
                healthImages[i].color = i + 1 <= playerHealth ? fullColor : blankColor;
            }
            if(playerHealth <= 0)
            {
                EndGame();
            }
        }
    }

    public int Score
    {
        get
        {
            return playerScore;
        }
        set
        {
            playerScore = value;
            scoreText.text = string.Format("Score: {0}", playerScore);
        }
    }

    public Vector3 FiringPosition
    {
        get
        {
            return transform.position + Vector3.up * firingOffset;
        }
    }

    private void Start()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));
        widthRange = screenBounds.x - transform.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.x;
        heightRange = screenBounds.z - transform.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.z;

        Score = 0;
        Health = healthImages.Length;
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    private void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, -widthRange, widthRange);
        viewPosition.z = Mathf.Clamp(viewPosition.z, -heightRange, heightRange);
        transform.position = viewPosition;
    }

    public void LoseHealth()
    {
        Health--;
    }

    public void AddPoints(int pointValue)
    {
        Score += pointValue;
    }

    void EndGame()
    {
        endPanel.SetActive(true);
        finalScoreText.text = string.Format("Final Score: {0}", Score);
        Time.timeScale = 0.0f;
        Destroy(gameObject);
    }
}
