using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_Backstabber : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_Backstabber passiveSkill_Backstabber = new PassiveSkill_Backstabber();

    public override Skill_Base GetSkillInstance() => passiveSkill_Backstabber.GetSkillInstance();
}
