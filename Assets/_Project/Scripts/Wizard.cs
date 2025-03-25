using UnityEngine;

public class Wizard : Character
{
    [SerializeField] private float _debuffChance = 0.3f;
    [SerializeField] private float _debuffDuration = 4f;
    [SerializeField] private float _damageReductionPercentage = 0.5f;

    public override void Attack(Character target, UIManager uIManager)
    {
        if (IsStunned())
        {
            Debug.Log($"{_characterName} is stunned and cannot attack!");
            return;
        }

        if (!target.IsAlive())
        {
            return;
        }

        target.TakeDamage(AttackPower);

        uIManager.ShowDamage(AttackPower, _characterName);

        if (Random.value < _debuffChance)
        {
            DamageReductionEffect debuffEffect = new DamageReductionEffect
            {
                Duration = _debuffDuration,
                ReductionPercentage = _damageReductionPercentage
            };
            target.ApplyEffect(debuffEffect, uIManager, _characterName);
        }
    }
}
