using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkillButton : MonoBehaviour
{
    public EffectObject_Base skill;
    public CharacterActionController target;

    public bool isAdd = false;
    
    private void Awake() {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick(){
        if (isAdd) {
            target.AddActiveEffect(skill);
        } else {
            target.ProcessEffect();
        }
    }
}
