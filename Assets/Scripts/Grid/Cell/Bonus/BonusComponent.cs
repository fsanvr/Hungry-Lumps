using UnityEngine;

public class BonusComponent : MonoBehaviour
{
    public Buff Buff { get; set; }
    
    public void Destroy(float seconds = 0.0f)
    {
        Destroy(this.gameObject, seconds);
    }
    
    public static BonusComponent Instantiate(GameObject parent, BonusData data)
    {
        var go = GameObject.Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = parent.transform;
        go.transform.localPosition = Vector3.zero;
        SetSprite(go, data.Sprite);
        SetBuff(go, data.Buff);
        return go.GetComponent<BonusComponent>();
    }
    
    private static void SetSprite(GameObject go, Sprite sprite)
    {
        var spriteRenderer = go.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.sprite = sprite;
        }
    }
    
    private static void SetBuff(GameObject go, Buff buff)
    {
        var bonusComponent = go.GetComponent<BonusComponent>();
        if (bonusComponent)
        {
            bonusComponent.Buff = buff;
        }
    }
}