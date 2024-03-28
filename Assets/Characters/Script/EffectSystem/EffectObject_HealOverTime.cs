using UnityEngine;

[CreateAssetMenu]
public class EffectObject_HealOverTime : EffectObject_Base
{
    [SerializeField]
    private Effect_HealOverTime effect_HealOverTime = new Effect_HealOverTime();

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null) => effect_HealOverTime.GetEffectInstance(caster);
}
