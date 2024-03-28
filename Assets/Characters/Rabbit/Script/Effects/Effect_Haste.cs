using System;
using UnityEngine;

[Serializable]
public class Effect_Haste : Effect_Base
{
    [SerializeField]
    private int durationTurn = 2;

    [SerializeField]
    private float moveSpeedMultiplier = 0.75f;

    [SerializeField]
    private float turnPassed = 0;

    public Effect_Haste(){

    }

    public Effect_Haste(Effect_Haste effect_Poison){
        this.durationTurn = effect_Poison.durationTurn;
        this.moveSpeedMultiplier = effect_Poison.moveSpeedMultiplier;
        this.turnPassed = effect_Poison.turnPassed;
        this.moveSpeedMultiplier = effect_Poison.moveSpeedMultiplier;
    }

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null)
    {
        return new Effect_Haste(this);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        if (to.GetCurrentHealth() > 0 && turnPassed == 0){
            Debug.Log("Hasting the target");
            to.GetCharacterData().moveSpeed.AddModifier(
                new StatModifier(
                    moveSpeedMultiplier,
                    StatModType.PercentAdd,
                    this
                )
            );
        }
        
        turnPassed += 1;
        
        if (turnPassed >= durationTurn){
            to.GetCharacterData().moveSpeed.RemoveAllModifiersFromSource(this);
        }

        return turnPassed >= durationTurn;
    }
}
