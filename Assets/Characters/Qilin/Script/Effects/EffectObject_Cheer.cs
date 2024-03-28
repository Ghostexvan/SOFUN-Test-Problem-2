using UnityEngine;

[CreateAssetMenu]
public class EffectObject_Cheer : EffectObject_Base
{
    [SerializeField]
    private Effect_Cheer effect_Cheer = new Effect_Cheer();

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null) => effect_Cheer.GetEffectInstance(caster);
}
