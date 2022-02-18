using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public sealed class UINavigationController : INavigationControllerInterface
    {
        public bool popUpEnabled;
        private Stack<GameObject> m_navigationStack = new Stack<GameObject>();

        public void Push(GameObject popup, bool newFlow = true, bool shouldActivateImmediately = true)
        {
            if (newFlow)
            {
                Pop();
            }
            else
            {
                GameObject popupToClose = m_navigationStack.Peek();
                DeactivatePopup(popupToClose);
            }

            m_navigationStack.Push(popup);
            if (!popUpEnabled) { popUpEnabled = true; }

            if (shouldActivateImmediately)
            {
                ActivatePopup(popup);
            }
        }

        public void Pop()
        {
            if (m_navigationStack.Count > 0)
            {
                GameObject popupToClose = m_navigationStack.Pop();
                DeactivatePopup(popupToClose);

            }
        }

        private void ActivatePopup(GameObject popup)
        {
            popup.SetActive(true);
        }

        private void DeactivatePopup(GameObject popup)
        {
            if (m_navigationStack.Count > 0)
            {
                GameObject popupToOpen = m_navigationStack.Peek();
                popupToOpen.SetActive(false);
            }
            else
            {
                popUpEnabled = false;
            }
        }
    }
}