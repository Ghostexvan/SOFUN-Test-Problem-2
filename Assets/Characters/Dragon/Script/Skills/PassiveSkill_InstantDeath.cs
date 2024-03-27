using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_InstantDeath : Skill_Base
{
    [SerializeField]
    private string skillName = "Instant Death";

    [SerializeField]
    private string description = "Instantly kills an enemy target if its HP is below 5%.";

    [SerializeField]
    private int turnCooldown = 3;

    private int turnCooldownLeft = 0;

    public PassiveSkill_InstantDeath(){

    }

    public PassiveSkill_InstantDeath(PassiveSkill_InstantDeath instantDeath){
        this.skillName = instantDeath.skillName;
        this.description = instantDeath.description;
        this.turnCooldown = instantDeath.turnCooldown;
        this.turnCooldownLeft = instantDeath.turnCooldownLeft;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (caster.GetCurrentHealth() <= 0){
            return false;
        }

        if (turnCooldownLeft > 0){
            turnCooldownLeft -= 1;
            return false;
        }

        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        Debug.Log("Checking condition for Instant Death");
        foreach(GameObject enemy in enemies){
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();
            
            if (controller.GetCurrentHealth() <= 0){
                continue;
            }

            // Debug.Log("Checking a target");
            // Debug.Log(controller.GetCurrentHealth() + " - " +
            //           controller.GetCharacterData().healthPoint.Value + " - " +
            //           controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value);

            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.05f){
                // Debug.Log("Condition meet!");
                return true;
            }
        }
        
        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_InstantDeath(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        // Debug.Log("Trying to get target for Instant Death");
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        // Debug.Log("List length got: " + enemies.Count);

        foreach(GameObject enemy in enemies){
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();

            if (controller.GetCurrentHealth() <= 0){
                continue;
            }

            // Debug.Log("Checking a target");
            // Debug.Log(controller.GetCurrentHealth() + " - " +
            //           controller.GetCharacterData().healthPoint.Value + " - " +
            //           controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value);
            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.05){
                // Debug.Log("Got an target");
                return new List<CharacterActionController>(){
                    enemy.GetComponent<CharacterActionController>()
                };
            };
        }

        Debug.Log("No target got");
        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        Debug.Log("Try to cast Instant Death");
        foreach(CharacterActionController target in targetList){
            target.DealPhysicalDamage(target.GetCharacterData().healthPoint.Value + target.GetCharacterData().physicalResistance.Value,
                                      caster.GetCharacterData().moveSpeed.Value);
        }

        turnCooldownLeft = turnCooldown;
    }
}
