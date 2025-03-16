using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour 
{
    public string characterName = "Character";
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;
    public bool isStunned = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Attack(Character target)
    {
        if (isStunned)
        {
            Debug.Log($"{characterName} is stunned and cannot attack!");
            return;
        }

        Debug.Log($"{characterName} attacks {target.characterName} for {attackDamage} damage.");

        target.TakeDamage(attackDamage);
    }

    public virtual void TakeDamage(int damage, bool isPoison = false)
    {
        currentHealth -= damage;

        Debug.Log($"{characterName} takes {damage} damage. HP: {currentHealth}/{maxHealth}");

        if (!isPoison)
        {
            EffectManager.Instance.CreateDamageText(damage, transform);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{characterName} has died!");

        gameObject.SetActive(false);
    }

    public virtual void ApplyStun(float duration)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"{characterName} is inactive. Cannot apply stun.");
            return;
        }

        StartCoroutine(StunEffect(duration));
        EffectManager.Instance.CreateEffectText("STUN", transform);
    }
    
    public virtual void ApplyPoison(int damagePerSecond, float duration)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"{characterName} is inactive. Cannot apply stun.");
            return;
        }

        StartCoroutine(PoisonEffect(damagePerSecond, duration));
    }
    
    public virtual void ApplyDebuff(float damageMultiplier, float duration)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"{characterName} is inactive. Cannot apply stun.");
            return;
        }

        StartCoroutine(DebuffEffect(damageMultiplier, duration));
        EffectManager.Instance.CreateEffectText("DEBUFF", transform);
    }

    private IEnumerator StunEffect(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);

        if (gameObject.activeInHierarchy)
        {
            isStunned = false;
        }
        else
        {
            Debug.LogWarning($"{characterName} is inactive. Stun effect terminated.");
        }
    }

    private IEnumerator PoisonEffect(int damagePerSecond, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning($"{characterName} is inactive. Poison effect terminated.");
                yield break;
            }

            TakeDamage(damagePerSecond, true);
            EffectManager.Instance.CreatePoisonDamageText(damagePerSecond, transform);
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DebuffEffect(float damageMultiplier, float duration)
    {
        int originalAttackDamage = attackDamage;

        attackDamage = Mathf.RoundToInt(attackDamage * damageMultiplier);
        yield return new WaitForSeconds(duration);

        if (gameObject.activeInHierarchy)
        {
            attackDamage = originalAttackDamage;
        }
        else
        {
            Debug.LogWarning($"{characterName} is inactive. Debuff effect termonated.");
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
