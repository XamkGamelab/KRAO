using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRectScaling : MonoBehaviour
{
    private List<Image> children;

    public void ScaleHeightUpByChildren(RectTransform _parent, RectTransform _scaling, float _posY)
    {
        // Put child images of _parent to list
        children = _parent.GetComponentsInChildren<Image>().ToList();

        float _height = 0;

        // Set _height as the height of the child images
        foreach (Image child in children)
        {
            _height += child.GetComponent<RectTransform>().rect.height;
        }

        // If the parent has a VerticalLayoutGroup add its spacing and paddings to _height
        if (_parent.GetComponent<VerticalLayoutGroup>() != null)
        {
            // Multiply spacing value by children count
            _height += children.Count * _parent.GetComponent<VerticalLayoutGroup>().spacing
            // Add paddings that affect height 
                + _parent.GetComponent<VerticalLayoutGroup>().padding.top
                + _parent.GetComponent<VerticalLayoutGroup>().padding.bottom;
        }

        // If there is a separate _scaling object, change its height to _height
        if (_scaling != null)
        {
            _scaling.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
        }
        //If not, scale the parent objects height
        else
        {
            _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
        }
    }
}