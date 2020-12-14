using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentFilter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HorizontalLayoutGroup hg = GetComponent<HorizontalLayoutGroup>();
        int childCount = transform.childCount -1;
        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        float width = hg.spacing * (childCount) + (childCount) * childWidth + hg.padding.left; // all the width are the same

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 265);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
