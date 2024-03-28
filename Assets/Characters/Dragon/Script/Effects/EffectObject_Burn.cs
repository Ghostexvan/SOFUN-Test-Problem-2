using UnityEngine;

[CreateAssetMenu]
public class EffectObject_Burn : EffectObject_Base
{
    [SerializeField]
    private Effect_Burn effect_Burn = new Effect_Burn();

    public override Effect_Base GetEffectInstance(CharacterActionController caster) => effect_Burn.GetEffectInstance(caster);
}
