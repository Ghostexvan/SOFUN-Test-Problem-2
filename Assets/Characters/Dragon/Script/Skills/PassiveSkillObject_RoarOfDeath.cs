using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_RoarOfDeath : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_RoarOfDeath passiveSkill_RoarOfDeath = new PassiveSkill_RoarOfDeath();

    public override Skill_Base GetSkillInstance() => passiveSkill_RoarOfDeath.GetSkillInstance();
}
