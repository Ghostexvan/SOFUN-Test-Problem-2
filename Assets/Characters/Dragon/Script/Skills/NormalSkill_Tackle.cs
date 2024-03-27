using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NormalSkill_Tackle : Skill_Base
{
    [SerializeField]
    private string skillName = "Tackle";

    [SerializeField]
    private string description = "Deals minor PD to the target.";

    [SerializeField]
    private bool getLowestHP = false;

    [SerializeField]
    private bool getLowestPR = false;

    [SerializeField]
    private bool getLowestMR = false;

    public NormalSkill_Tackle(){

    }

    public NormalSkill_Tackle(NormalSkill_Tackle normalSkill_Tackle){
        this.skillName = normalSkill_Tackle.skillName;
        this.description = normalSkill_Tackle.description;
        this.getLowestHP = normalSkill_Tackle.getLowestHP;
        this.getLowestPR = normalSkill_Tackle.getLowestPR;
        this.getLowestMR = normalSkill_Tackle.getLowestMR;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new NormalSkill_Tackle(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        GameObject target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

        if (this.getLowestHP || this.getLowestPR || this.getLowestMR){
            foreach(GameObject enemy in enemies){
                CharacterActionController targetController = target.GetComponent<CharacterActionController>();
                CharacterActionController controller = enemy.GetComponent<CharacterActionController>();

                if (controller.GetCurrentHealth() <= 0){
                    continue;
                }

                if (this.getLowestHP && controller.GetCurrentHealth() < targetController.GetCurrentHealth()){
                    target = enemy;
                } else if (this.getLowestMR && controller.GetCharacterData().magicalResistance.Value < targetController.GetCharacterData().magicalResistance.Value){
                    target = enemy;
                } else if (this.getLowestPR && controller.GetCharacterData().physicalResistance.Value < targetController.GetCharacterData().physicalResistance.Value){
                    target = enemy;
                }
            }
        }
        
        return new List<CharacterActionController>(){
            target.GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.DealPhysicalDamage(caster.GetCharacterData().physicalDamage.Value, caster.GetCharacterData().moveSpeed.Value);
        }

        caster.ChangeCurrentManaPoint(50);
    }
}
