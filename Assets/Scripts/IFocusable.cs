using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFocusable
{
    public bool IsFocused { get; }
    public Transform FocusTransform { get; }
    
    public void Focus();
    public void Unfocus();
    
}
