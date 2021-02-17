using UnityEngine;

public interface INavigationControllerInterface
{
    void Push(GameObject popup, bool newFlow = true, bool shouldActivateImmediately = true);
    void Pop();
}


