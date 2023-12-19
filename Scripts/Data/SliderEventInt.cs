/*
 USTRAP
 DATA CLASS
 SLIDER EVENT INT
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for int slider events in the inspector.
    /// </summary>
    [Serializable]
    public class SliderEventInt : UIElemementValueChangedEvent<SliderInt, int>
    {
    }
}