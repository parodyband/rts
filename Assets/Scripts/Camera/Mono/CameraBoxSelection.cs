using UnityEngine;
using UnityEngine.UI;

public class CameraBoxSelection : Singleton<CameraBoxSelection>
{
    public Image selectionBoxImage;
    private Vector2 m_StartPos;
    private Rect m_SelectionBox;

    private Vector2 m_AnchorMin;
    private Vector2 m_AnchorMax;

    public bool MakingSelection { private set; get; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(selectionBoxImage.rectTransform.parent as RectTransform, Input.mousePosition, null, out Vector2 localPointStart);
            m_StartPos = localPointStart;
            selectionBoxImage.gameObject.SetActive(true);
            MakingSelection = false;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionBoxImage.gameObject.SetActive(false);
        }

        MakingSelection = Input.GetMouseButton(0);
    }

    private void UpdateSelectionBox(Vector2 currentPos)
    {
        if (!selectionBoxImage.gameObject.activeInHierarchy)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(selectionBoxImage.rectTransform.parent as RectTransform, currentPos, null, out Vector2 localPointCurrent);

        m_AnchorMin.x = Mathf.Min(localPointCurrent.x, m_StartPos.x);
        m_AnchorMin.y = Mathf.Min(localPointCurrent.y, m_StartPos.y);
        m_AnchorMax.x = Mathf.Max(localPointCurrent.x, m_StartPos.x);
        m_AnchorMax.y = Mathf.Max(localPointCurrent.y, m_StartPos.y);

        selectionBoxImage.rectTransform.anchoredPosition = m_AnchorMin;
        selectionBoxImage.rectTransform.sizeDelta = m_AnchorMax - m_AnchorMin;

        m_SelectionBox = new Rect(m_AnchorMin, m_AnchorMax - m_AnchorMin);
    }

    public Rect SelectionBox()
    {
        // Convert the local min and max to screen space
        RectTransform parentRectTransform = (RectTransform)selectionBoxImage.rectTransform.parent;
        Vector2 minScreenPoint = RectTransformToScreenSpace(parentRectTransform, m_SelectionBox.min);
        Vector2 maxScreenPoint = RectTransformToScreenSpace(parentRectTransform, m_SelectionBox.max);

        // Create a Rect in screen space
        return Rect.MinMaxRect(minScreenPoint.x, minScreenPoint.y, maxScreenPoint.x, maxScreenPoint.y);
    }

    private Vector2 RectTransformToScreenSpace(RectTransform parentRectTransform, Vector2 localPoint)
    {
        // Convert the local point to a normalized point relative to the parent RectTransform
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x - parentRectTransform.rect.x) / parentRectTransform.rect.width,
            (localPoint.y - parentRectTransform.rect.y) / parentRectTransform.rect.height
        );

        // Convert the normalized point to screen space
        return new Vector2(
            normalizedPoint.x * Screen.width,
            normalizedPoint.y * Screen.height
        );
    }


}