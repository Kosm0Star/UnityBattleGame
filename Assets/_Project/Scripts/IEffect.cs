using UnityEngine;

public interface IEffect
{
    float Duration { get; set; }
    string SourceName { get; set; }
    Character Target { get; set; }
    void Apply(Character character);
    void Update();
}
