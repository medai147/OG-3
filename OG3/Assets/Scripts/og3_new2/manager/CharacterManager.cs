using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�L�����N�^�[�̏�Ԃ�A�j���[�V�������Ǘ�
//�L�����N�^�[�̍D���x��A�j���[�V�����t���O�𐧌�
public class CharacterManager
{
    private Dictionary<string, int> affectionPoints;

    public void InitializeCharacters(List<string> characters)
    {
        foreach (string character in characters)
        {
            affectionPoints[character] = 0; // �����D���x��ݒ�
        }
    }

    public int GetAffection(string character)
    {
        return affectionPoints.ContainsKey(character) ? affectionPoints[character] : 0;
    }

    public void UpdateAffection(string character, int change)
    {
        if (affectionPoints.ContainsKey(character))
        {
            affectionPoints[character] += change;
        }
    }
}

