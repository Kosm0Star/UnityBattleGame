using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public GameObject damageTextPrefab;
    public GameObject poisonDamageTextPrefab;
    public GameObject effectTextPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateDamageText(int damage, Transform target)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 1f);
        screenPosition.z = 0;

        GameObject damageTextObject = Instantiate(damageTextPrefab, screenPosition, Quaternion.identity);
        damageTextObject.transform.SetParent(GameObject.Find("EffectCanvas").transform);
        

        if (damageTextObject.TryGetComponent<TextMeshProUGUI>(out var textComponent))
        {
            textComponent.text = damage.ToString();
        }

        Destroy(damageTextObject, 0.5f);
    }


    public void CreateEffectText(string effectName, Transform target)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 1f);
        screenPosition.z = 0;

        GameObject effectTextObject = Instantiate(effectTextPrefab, screenPosition, Quaternion.identity);
        effectTextObject.transform.SetParent(GameObject.Find("EffectCanvas").transform);


        if (effectTextObject.TryGetComponent<TextMeshProUGUI>(out var textComponent))
        {
            textComponent.text = effectName.ToString();
        }

        Destroy(effectTextObject, 1f);
    }

    public void CreatePoisonDamageText(int damage, Transform target)
    { 
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);
        screenPosition.z = 0;

        GameObject poisonDamageTextObject = Instantiate(poisonDamageTextPrefab, screenPosition, Quaternion.identity);
        poisonDamageTextObject.transform.SetParent(GameObject.Find("EffectCanvas").transform);

        poisonDamageTextObject.transform.localScale = Vector3.one;

        if (poisonDamageTextObject.TryGetComponent<TextMeshProUGUI>(out var textComponent))
        {
            textComponent.text = $"{damage}";
        }

        Destroy(poisonDamageTextObject, 1f);
    }
}
