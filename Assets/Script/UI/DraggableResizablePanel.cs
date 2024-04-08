using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableResizablePanel : MonoBehaviour, IDragHandler
{
    private RectTransform panelRectTransform;
    private Vector2 lastMousePosition;

    public void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();

        Vector3[] panelCorners = new Vector3[4];
        panelRectTransform.GetWorldCorners(panelCorners);

        Vector3 bottomLeftCorner = panelCorners[0];
        Vector3 screenBottomLeftCorner = new Vector3(0, 0, bottomLeftCorner.z);

        Vector3 panelDistanceFromScreenBottomLeft = bottomLeftCorner - screenBottomLeftCorner;

        

        lastMousePosition = panelDistanceFromScreenBottomLeft;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, currentMousePosition, eventData.pressEventCamera, out Vector2 localPoint))
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {

                //print("currentMousePosition: "+ currentMousePosition + " lastMousePosition: " + lastMousePosition + " anchoredPosition: " + panelRectTransform.anchoredPosition+ " diff: "+ diff);

                panelRectTransform.anchoredPosition += diff;
            }
        }

        lastMousePosition = currentMousePosition;
    }
}
