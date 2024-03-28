using UnityEngine;

[CreateAssetMenu]
public class EffectObject_Haste : EffectObject_Base
{
    [SerializeField]
    private Effect_Haste effect_Haste = new Effect_Haste();

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null) => effect_Haste.GetEffectInstance(caster);
}
