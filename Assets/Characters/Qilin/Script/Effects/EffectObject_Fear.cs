using UnityEngine;

[CreateAssetMenu]
public class EffectObject_Fear : EffectObject_Base
{
    [SerializeField]
    private Effect_Fear effect_Fear = new Effect_Fear();

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null) => effect_Fear.GetEffectInstance(caster);
}
