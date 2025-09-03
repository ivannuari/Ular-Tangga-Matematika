using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputNamaPemainPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button proceedButton;

    private void Start()
    {
        if(!string.IsNullOrEmpty(GameManager.Instance.currentName))
        {
            nameInput.text = GameManager.Instance.currentName;
        }

        proceedButton.onClick.AddListener(() =>
        {
            if(string.IsNullOrEmpty(nameInput.text))
            {
                return;
            }

            //input nama ke game manager
            GameManager.Instance.currentName = nameInput.text;
            //close panel ini
            gameObject.SetActive(false);
        });
    }
}
