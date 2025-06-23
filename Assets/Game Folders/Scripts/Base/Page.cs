using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GaweDeweStudio
{
    public class Page : MonoBehaviour
    {
        public PageName nama;
        protected Button[] allButtons;

        protected virtual void Start()
        {
            allButtons = GetComponentsInChildren<Button>(true);

            if(allButtons.Length > 0)
            {
                foreach (var item in allButtons)
                {
                    item.onClick.AddListener(() => AudioManager.Instance?.MainkanSuara("klik"));
                }
            }
        }

        protected void ChangeScene(string namaScene)
        {
            SceneManager.LoadSceneAsync(namaScene , LoadSceneMode.Single);
        }

        public virtual void SetNomor(int nomor)
        {

        }
    }
}


public enum PageName
{
    Menu,
    Tutorial,
    Tentang,
    Highscore,
    Level,
    SubLevel,
    Belajar,
    Game,
    Soal,
    Result,
    GoToHome,
    Restart,
    Exit
}

