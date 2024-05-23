using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public string elementName;
    public int elementCount;
    public Color elementColor;
    public string elementDescription;

    public Transform elementTransform;
    private GameObject elementPrefab;

    private void Start()
    {
        // Don't destroy on load if this is a specific element
        if (elementName == "ElementPrefab")
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateCellInterface()
    {
        if (elementPrefab == null)
        {
            elementPrefab = GameObject.Find("ElementPrefab");
        }

        if (elementPrefab == null)
        {
            Debug.Log("ElementPrefab is null");
            return;
        }

        if (elementCount == 0)
        {
            if (elementTransform != null)
            {
                Debug.Log("Destroying elementPrefab", elementPrefab);
                Destroy(elementTransform.gameObject);
                elementTransform = null;
            }
            return;
        }
        else
        {
            if (elementTransform == null)
            {
                // Spawn a new element prefab
                Transform newElement = Instantiate(elementPrefab).transform;
                newElement.SetParent(transform, false);
                newElement.localPosition = Vector3.zero;
                newElement.localScale = Vector3.one;
                elementTransform = newElement;
            }

            // Initialize UI elements
            Image bgImage = SimpleMethods.getChildByTag(elementTransform, "backgroundImage").GetComponent<Image>();
            Text elementText = SimpleMethods.getChildByTag(elementTransform, "elementText").GetComponent<Text>();
            Text amountText = SimpleMethods.getChildByTag(elementTransform, "amountText").GetComponent<Text>();

            // Disable Raycast Target on text elements
            elementText.raycastTarget = false;
            amountText.raycastTarget = false;

            // Change UI options
            bgImage.color = elementColor;
            elementText.text = elementName;
            amountText.text = elementCount.ToString();

			// change size elementText
			RectTransform rectTransform = elementText.GetComponent<RectTransform>();
			rectTransform.sizeDelta = new Vector2(83, 83); 
			rectTransform.anchorMin = new Vector2(0.5f, 0.5f); 
			rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransform.pivot = new Vector2(0.5f, 0.5f); 
        }
    }

    // Change element options
    public void ChangeElement(string name, int count, Color color, string description)
    {
        elementName = name;
        elementCount = count;
        elementColor = color;
        elementDescription = description;
        UpdateCellInterface();
    }

    // Clear element
    public void ClearElement()
    {
        elementName = "";
        elementCount = 0;
        elementColor = Color.clear; // Use Color.clear for a "transparent" color
        elementDescription = "";
        UpdateCellInterface();
    }
}
