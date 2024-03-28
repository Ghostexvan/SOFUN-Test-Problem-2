using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_GuidanceOfFire : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_GuidanceOfFire passiveSkill_GuidanceOfFire = new PassiveSkill_GuidanceOfFire();
    
    public override Skill_Base GetSkillInstance() => passiveSkill_GuidanceOfFire.GetSkillInstance();
}
