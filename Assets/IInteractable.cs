using UnityEngine;

internal interface IInteractable
{
    void DoInteraction();
    void UndoInteraction();

    GameObject GetGameObject();
}
