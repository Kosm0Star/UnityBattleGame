using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public abstract class Character : MonoBehaviour
{
    [HideInInspector] public float health;
    [SerializeField] protected float _initialHealth;
    [SerializeField] protected float _attack;
    [SerializeField] protected string _characterName;
    private TMP_Text _healthText;

    protected List<IEffect> currentEffects = new List<IEffect>();
    private bool _isStunned = false;

    public bool IsStunned() => _isStunned;

    private void Start() => ResetHealth();

    public void ResetHealth()
    {
        health = _initialHealth;
        UpdateHealthText();
        ResetEffects();
        Debug.Log($"{_characterName}: Health reset to {health}");
    }

    public void ResetEffects()
    {
        var effectsToRevert = new List<IEffect>(currentEffects);

        foreach (var effect in effectsToRevert)
        {
            if (effect is DamageReductionEffect debuff && debuff.Target != null)
            {
                debuff.Revert();
                Debug.Log($"{_characterName}: Reverted effect {effect.GetType().Name}");
            }
        }

        currentEffects.Clear();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }

            UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (_healthText != null)
        {
            _healthText.text = $"{_characterName} Health: {health}";
        }
    }

    public void SetHealthText(TMP_Text newText)
    {
        _healthText = newText;
        UpdateHealthText();
    }

    public abstract void Attack(Character target, UIManager uiManager);

    public void ApplyEffect(IEffect effect, UIManager uIManager, string sourceName)
    {
        var existingEffect = currentEffects.FirstOrDefault(e => e.GetType() == effect.GetType());

        if (existingEffect != null)
        {
            if (existingEffect is DamageReductionEffect oldDebuff && oldDebuff.Target != null)
            {
                oldDebuff.Revert();
            }

            currentEffects.Remove(existingEffect);
        }

        currentEffects.Add(effect);
        effect.SourceName = sourceName;
        effect.Target = this;
        effect.Apply(this);

        StartCoroutine(uIManager.ShowEffect($"Applied {effect.GetType().Name}", sourceName));
    }

    public virtual void Update()
    {
            for (int i = currentEffects.Count - 1; i >= 0; i--)
            {
                var effect = currentEffects[i];
                effect.Update();

                if (effect.Duration <= 0)
                {
                    if (effect is StunEffect)
                    {
                        _isStunned = false;
                    }

                    if (effect is DamageReductionEffect debuff && debuff.Target != null)
                    {
                        debuff.Revert();
                    }

                    currentEffects.RemoveAt(i);
                }
            }
        
    }

    public bool IsAlive() => health > 0;

    protected virtual void Die() => Debug.Log($"{_characterName} has died.");

    public string GetName() => _characterName;

    public float AttackPower
    {
        get => _attack;
        set => _attack = value;
    }
}
