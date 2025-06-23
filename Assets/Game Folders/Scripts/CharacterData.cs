[System.Serializable]
public class CharacterData
{
    public PlayerType tipe = PlayerType.Pemain;
    public int characterNumber;
    public string characterName;
    public int characterPosition;
    public int characterSkin;
}

public enum PlayerType
{
    Pemain,
    Ai
}