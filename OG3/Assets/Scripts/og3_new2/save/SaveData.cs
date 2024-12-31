using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string heroineName; // �q���C����
    public int currentStoryIndex; // ���݂̃X�g�[���[�ԍ�
    public Dictionary<string, int> affectionPoints; // �D���x�f�[�^
    public string screenshotPath; // �X�N���[���V���b�g�̃p�X
    public string saveTimestamp; // �Z�[�u���� (ISO8601�`��)
}
