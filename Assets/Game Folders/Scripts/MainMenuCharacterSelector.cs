using System;
using UnityEngine;

public class MainMenuCharacterSelector : MonoBehaviour
{
    [System.Serializable]
    public class MenuCharacters
    {
        public int id;
        public GameObject[] allCharaObject;
    }

    [SerializeField] private MenuCharacters[] allCharacters;
    public event Action<int, int> OnCharacterUpdated;

    public void ChangeCharacter(int charaId, int index)
    {
        foreach (var character in allCharacters[charaId].allCharaObject) 
        {
            character.SetActive(false);
        }

        allCharacters[charaId].allCharaObject[index].SetActive(true);

        OnCharacterUpdated?.Invoke(charaId, index);
    }
}
