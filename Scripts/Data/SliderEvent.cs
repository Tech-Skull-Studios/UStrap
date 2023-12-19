/*
 USTRAP
 DATA CLASS
 SLIDER EVENT
 v1.1
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for slider events in the inspector.
    /// </summary>
    [Serializable]
    public class SliderEvent : UIElemementValueChangedEvent<Slider, float>
    {
    }
}