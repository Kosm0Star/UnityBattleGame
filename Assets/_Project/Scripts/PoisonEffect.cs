using UnityEngine;

public class PoisonEffect : IEffect
{
    public float Duration { get; set; }
    public float DamagePerTick { get; set; }
    public string SourceName { get; set; }
    public Character Target { get; set; }

    private float _timeSinceLastTick = 0f;

    public void Apply(Character character)
    {
        Target = character;
        Debug.Log($"{SourceName} applied poison to {Target.GetName()}!");
    }

    public void Update()
    {
        if (Duration > 0 && Target != null)
        {
            _timeSinceLastTick += Time.deltaTime;

            if (_timeSinceLastTick >= 1f)
            {
                Target.TakeDamage(DamagePerTick);
                Debug.Log($"Poison deals {DamagePerTick} damage to {Target.GetName()}.");
                _timeSinceLastTick = 0f;
            }

            Duration -= Time.deltaTime;
        }
    }
}
