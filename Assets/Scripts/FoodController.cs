using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public Food food;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
