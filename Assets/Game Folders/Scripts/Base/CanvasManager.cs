using System;
using UnityEngine;

namespace GaweDeweStudio
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] protected Page[] pages;

        public virtual void SetPage(PageName findPage)
        {
            foreach (var page in pages)
            {
                page.gameObject.SetActive(false);
            }

            int temp = Array.FindIndex(pages, p => p.nama == findPage);
            pages[temp].gameObject.SetActive(true);
        }

        public virtual void SetPage(PageName findPage , int nomor)
        {
            foreach (var page in pages)
            {
                page.gameObject.SetActive(false);
            }

            int temp = Array.FindIndex(pages, p => p.nama == findPage);
            pages[temp].gameObject.SetActive(true);
            pages[temp].SetNomor(nomor);
        }
    }
}