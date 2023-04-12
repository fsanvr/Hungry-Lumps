using UnityEngine;

public class FoodComponent : MonoBehaviour
{
    public float Prize { get; set; }

    public void Destroy(float seconds = 0.0f)
    {
        Destroy(this.gameObject, seconds);
    }

    public static FoodComponent Instantiate(GameObject parent, FoodData data)
    {
        var go = GameObject.Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = parent.transform;
        go.transform.localPosition = Vector3.zero;
        SetSprite(go, data.Sprite);
        SetPrize(go, data.Prize);
        SetCountdown(go, data.TimeToDestroy);
        return go.GetComponent<FoodComponent>();
    }

    private static void SetSprite(GameObject go, Sprite sprite)
    {
        var spriteRenderer = go.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.sprite = sprite;
        }
    }
    
    private static void SetPrize(GameObject go, float prize)
    {
        var foodComponent = go.GetComponent<FoodComponent>();
        if (foodComponent)
        {
            foodComponent.Prize = prize;
        }
    }
    
    private static void SetCountdown(GameObject go, float timeToDestroy)
    {
        var foodComponent = go.GetComponent<FoodComponent>();
        if (foodComponent)
        {
            foodComponent.Destroy(timeToDestroy);
        }
    }
}