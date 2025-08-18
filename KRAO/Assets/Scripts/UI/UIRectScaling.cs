using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRectScaling : MonoBehaviour
{
    private List<Image> children;

    public void ScaleHeightUpByChildren(RectTransform _parent, RectTransform _scaling, float _posY)
    {
        children = _parent.GetComponentsInChildren<Image>().ToList();

        float _height = 0;

        foreach (Image child in children)
        {
            _height += child.GetComponent<RectTransform>().rect.height;
        }

        if (_parent.GetComponent<VerticalLayoutGroup>() != null)
        {
            _height += children.Count * _parent.GetComponent<VerticalLayoutGroup>().spacing
                + _parent.GetComponent<VerticalLayoutGroup>().padding.top
                + _parent.GetComponent<VerticalLayoutGroup>().padding.bottom;
        }

        if (_scaling != null)
        {
            _scaling.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
            _scaling.transform.position = new Vector3(_scaling.transform.position.x,
                _posY + (_height * .5f), _scaling.transform.position.z);
        }
        else
        {
            _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
        }
    }
}