using UnityEngine;

[CreateAssetMenu]
public class NormalSkillObject_Tackle : SkillObject_Base
{
    [SerializeField]
    private NormalSkill_Tackle normalSkill_Tackle = new NormalSkill_Tackle();

    public override Skill_Base GetSkillInstance() => normalSkill_Tackle.GetSkillInstance();
}
