using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_EncouragingDance : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_EncouragingDance passiveSkill_EncouragingDance = new PassiveSkill_EncouragingDance();

    public override Skill_Base GetSkillInstance() => passiveSkill_EncouragingDance.GetSkillInstance();
}
