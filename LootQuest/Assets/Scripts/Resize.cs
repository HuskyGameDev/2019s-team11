using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Resize : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    public Vector2 minSize;
    public Vector2 maxSize;

    private RectTransform rectTransform;
    private Vector2 currentPointerPosition;
    private Vector2 previousPointerPosition;

    //initialize rectTransform
    void Awake()
    {
        rectTransform = transform.parent.GetComponent<RectTransform>();
    }

    //sets the transform data to the last sibling to be used to transform the screen space to point to a position in the local space
    public void OnPointerDown(PointerEventData data)
    {
        rectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        if (rectTransform == null)
            return;

        //gets the delta value of the previous rectTransform
        Vector2 sizeDelta = rectTransform.sizeDelta;

        //finds the resize value by finding the difference between current and prev position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
            );

        //stores for a future run
        rectTransform.sizeDelta = sizeDelta;

        //the old current is the new prev
        previousPointerPosition = currentPointerPosition;
    }
}