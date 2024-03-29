using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_WishingLuck : Skill_Base
{
    [SerializeField]
    private string skillName = "Wishing Luck";

    [SerializeField]
    private string description = "Heals an ally with lower than 10% HP by 50% own HP.";

    [SerializeField]
    private float healthPointMultiplier = 0.5f;

    [SerializeField]
    private int cooldownTurns = 3;

    private int cooldownLeft = 0;

    public PassiveSkill_WishingLuck(){

    }

    public PassiveSkill_WishingLuck(PassiveSkill_WishingLuck passiveSkill_WishingLuck){
        this.skillName = passiveSkill_WishingLuck.skillName;
        this.description = passiveSkill_WishingLuck.description;
        this.healthPointMultiplier = passiveSkill_WishingLuck.healthPointMultiplier;
        this.cooldownLeft = passiveSkill_WishingLuck.cooldownLeft;
        this.cooldownTurns = passiveSkill_WishingLuck.cooldownTurns;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }

        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        foreach (GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();
            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.1f){
                return cooldownLeft == 0 && caster.GetCurrentHealth() > 0;
            }
        }

        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_WishingLuck(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        foreach (GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();
            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.1f){
                return new List<CharacterActionController>(){
                    controller
                };
            }
        }

        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.ChangeCurrentHealthPoint(caster.GetCharacterData().healthPoint.Value * healthPointMultiplier);
        }

        cooldownLeft = cooldownTurns;
    }
}
