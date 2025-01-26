using Scripts.Events.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class DuelController : MonoBehaviour
    {
        [SerializeField]
        private Image duelImage;
        private bool isProcessing = false;

        public void DuelAppear()
        {
            if(!isProcessing)
                StartCoroutine(imageAppear());
        }

        private IEnumerator imageAppear()
        {
            isProcessing = true;

            duelImage.enabled = true;
            yield return new WaitForSeconds(2);
            duelImage.enabled = false;

            isProcessing = false;
        }
    }
}
