using UnityEngine;

public class StunEffect : IEffect
{
    public float Duration { get; set; }
    public string SourceName { get; set; }
    public Character Target { get; set; }

    public void Apply(Character character)
    {
        Target = character;
        Debug.Log($"{SourceName} stunned {Target.GetName()}!");
    }

    public void Update() => Duration = Mathf.Max(Duration - Time.deltaTime, 0);
}
