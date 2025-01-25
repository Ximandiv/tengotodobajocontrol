using Scripts.Events.Cutscenes;
using Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class StaticCutscenesController : MonoBehaviour
    {
        [SerializeField]
        private List<Image> staticCutscenes = new List<Image>();

        [SerializeField]
        private GameStatus gameStatus;

        [SerializeField]
        private int currentCutsceneIndex = 3;
        [SerializeField]
        private bool canClick = true;
        [SerializeField]
        private bool finished = false;

        private void Awake()
        {
            gameStatus.GameStarted = true;
            gameStatus.StartCutsceneEnd = false;

            foreach (Transform cutscene in transform)
            {
                var imageComponent = cutscene.GetComponent<Image>();

                if (imageComponent is null) continue;

                staticCutscenes.Add(imageComponent);
            }
        }

        private void Update()
        {
            if (!canClick) return;

            if (Input.GetMouseButtonDown(0))
            {
                canClick = false;

                var currentCutscene = staticCutscenes[currentCutsceneIndex];
                currentCutscene.gameObject.SetActive(false);

                if (!finished)
                    currentCutsceneIndex--;

                StartCoroutine(WaitBeforeClickAgain());
            }
        }

        private IEnumerator WaitBeforeClickAgain()
        {
            yield return new WaitForSeconds(1);

            if (finished)
            {
                gameStatus.StartCutsceneEnd = true;
                CutsceneEvents.InvokeOnStart();

                Destroy(gameObject);
                yield return null;
            }
            else if (currentCutsceneIndex == 0)
                finished = true;

            canClick = true;
        }
    }
}
