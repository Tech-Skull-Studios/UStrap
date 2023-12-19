/*
 USTRAP
 DATA CLASS
 LABEL EVENT
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for label events in the inspector.
    /// </summary>
    [Serializable]
    public class LabelEvent : UIElemementValueChangedEvent<Label, string>
    {
    }
}