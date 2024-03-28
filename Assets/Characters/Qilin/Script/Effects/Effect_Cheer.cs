using System;
using UnityEngine;

[Serializable]
public class Effect_Cheer : Effect_Base
{
    [SerializeField]
    private int durationTurn = 2;

    [SerializeField]
    private float magicalDamageMultiplier = 0.25f;

    [SerializeField]
    private float turnPassed = 0;

    public Effect_Cheer(){

    }

    public Effect_Cheer(Effect_Cheer effect_Cheer){
        this.durationTurn = effect_Cheer.durationTurn;
        this.turnPassed = effect_Cheer.turnPassed;
        this.magicalDamageMultiplier = effect_Cheer.magicalDamageMultiplier;
    }

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null)
    {
        return new Effect_Cheer(this);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        if (to.GetCurrentHealth() > 0){
            if (turnPassed == 0){
                Debug.Log("Cheer the target");
                to.GetCharacterData().magicalDamage.AddModifier(
                    new StatModifier(
                        magicalDamageMultiplier,
                        StatModType.PercentAdd,
                        this
                    )
                );
            }
        }
        
        turnPassed += 1;
        
        if (turnPassed >= durationTurn){
            to.GetCharacterData().magicalDamage.RemoveAllModifiersFromSource(this);
        }

        return turnPassed >= durationTurn;
    }
}
