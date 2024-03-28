using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_BlockClouds : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_BlockClouds passiveSkill_BlockClouds = new PassiveSkill_BlockClouds();

    public override Skill_Base GetSkillInstance() => passiveSkill_BlockClouds.GetSkillInstance();
}
