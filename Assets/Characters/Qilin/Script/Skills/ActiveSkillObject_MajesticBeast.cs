using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_MajesticBeast : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_MajesticBeast activeSkill_MajesticBeast = new ActiveSkill_MajesticBeast();

    public override Skill_Base GetSkillInstance() => activeSkill_MajesticBeast.GetSkillInstance();
}
