using System;
using UnityEngine;

[Serializable]
public class Effect_Fear : Effect_Base
{
    [SerializeField]
    private int durationTurn = 2;

    [SerializeField]
    private float movementMultiplier = 0.5f;

    [SerializeField]
    private float turnPassed = 0;

    public Effect_Fear(){

    }

    public Effect_Fear(Effect_Fear effect_Fear){
        this.durationTurn = effect_Fear.durationTurn;
        this.turnPassed = effect_Fear.turnPassed;
        this.movementMultiplier = effect_Fear.movementMultiplier;
    }

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null)
    {
        return new Effect_Fear(this);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        if (to.GetCurrentHealth() > 0){
            if (turnPassed == 0){
                Debug.Log("Fear the target");
                to.GetCharacterData().moveSpeed.AddModifier(
                    new StatModifier(
                        -movementMultiplier,
                        StatModType.PercentAdd,
                        this
                    )
                );
            }
        }
        
        turnPassed += 1;
        
        if (turnPassed >= durationTurn){
            to.GetCharacterData().moveSpeed.RemoveAllModifiersFromSource(this);
        }

        return turnPassed >= durationTurn;
    }
}
