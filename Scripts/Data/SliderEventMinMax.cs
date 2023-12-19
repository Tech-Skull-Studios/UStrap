/*
 USTRAP
 DATA CLASS
 SLIDER EVENT MIN MAX
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for min max slider events in the inspector.
    /// </summary>
    [Serializable]
    public class SliderEventMinMax : UIElemementValueChangedEvent<MinMaxSlider, Vector2>
    {
    }
}