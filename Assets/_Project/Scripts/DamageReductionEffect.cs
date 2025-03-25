using UnityEngine;

public class DamageReductionEffect : IEffect
{
    public float Duration { get; set; }
    public float ReductionPercentage { get; set; }
    public string SourceName { get; set; }
    public Character Target { get; set; }

    public void Apply(Character character)
    {
        Target = character;
        if (Target == null)
        {
            Debug.LogError("Attempted to apply DamageReductionEffect to a null target!");
            return;
        }
        Debug.Log($"{SourceName} applied damage reduction to {Target.GetName()}");
        Target.AttackPower *= ReductionPercentage;
    }

    public void Update()
    {
        Duration = Mathf.Max(Duration - Time.deltaTime, 0);
    }

    public void Revert()
    {
        if (Target != null)
        {
            Target.AttackPower /= ReductionPercentage;
            Debug.Log($"[{SourceName}] reverted damage reduction for {Target.GetName()} (AttackPower restored to {Target.AttackPower})");
        }
    }
}
