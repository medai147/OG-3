using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�I�����Ɋւ��鏈�����Ǘ�����N���X
//�����t���I�����̕\����I�����ʂ̏�����S��
public class ChoiceManager
{
    private List<ChoiceData> choices;

    public void LoadChoices(string csvPath)
    {
        // CSV��ǂݍ��݁AChoiceData�I�u�W�F�N�g�̃��X�g�ɕϊ�
    }

    public List<ChoiceData> GetChoicesForSelectID(int selectID)
    {
        // �w�肵���I����ID�ɑΉ�����I�������X�g��Ԃ�
        return choices;
    }
}

public class ChoiceData
{
    public int SelectID;
    public string Text;
    public int NextSceneID;
    public string TargetCharacter;
    public int AffectionChange;
    public string Condition;
}

