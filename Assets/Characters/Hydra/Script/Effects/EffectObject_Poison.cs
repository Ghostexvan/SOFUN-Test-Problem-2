using UnityEngine;

[CreateAssetMenu]
public class EffectObject_Poison : EffectObject_Base
{
    [SerializeField]
    private Effect_Poison effect_Poison = new Effect_Poison();

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null) => effect_Poison.GetEffectInstance(caster);
}
