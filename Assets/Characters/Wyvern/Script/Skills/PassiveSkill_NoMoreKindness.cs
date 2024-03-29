using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_NoMoreKindness : Skill_Base
{
    [SerializeField]
    private string skillName = "No More Kindness";

    [SerializeField]
    private string description = "Deals 75% HP as PD to an enemy with highest PD when below 30% HP.";

    [SerializeField]
    private float healthPointMultiplier = 0.75f;

    [SerializeField]
    private int cooldownTurns = 3;

    private int cooldownLeft = 0;

    public PassiveSkill_NoMoreKindness()
    {

    }

    public PassiveSkill_NoMoreKindness(PassiveSkill_NoMoreKindness passiveSkill_NoMoreKindness)
    {
        this.skillName = passiveSkill_NoMoreKindness.skillName;
        this.description = passiveSkill_NoMoreKindness.description;
        this.cooldownLeft = passiveSkill_NoMoreKindness.cooldownLeft;
        this.healthPointMultiplier = passiveSkill_NoMoreKindness.healthPointMultiplier;
        this.cooldownTurns = passiveSkill_NoMoreKindness.cooldownTurns;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }
        
        return cooldownLeft == 0 && 
               caster.GetCurrentHealth() > 0 && 
               caster.GetCurrentHealth() / caster.GetCharacterData().healthPoint.Value <= 0.3f;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_NoMoreKindness(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        return new List<CharacterActionController>(){
            enemies[UnityEngine.Random.Range(0, enemies.Count)].GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach (CharacterActionController target in targetList)
        {
            target.DealPhysicalDamage(
                healthPointMultiplier * caster.GetCharacterData().healthPoint.Value,
                caster.GetCharacterData().moveSpeed.Value
            );
        }

        cooldownLeft = cooldownTurns;
    }
}
