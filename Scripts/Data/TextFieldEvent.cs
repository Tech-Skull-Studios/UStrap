/*
 USTRAP
 DATA CLASS
 TEXT FIELD EVENT
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for text field events in the inspector.
    /// </summary>
    [Serializable]
    public class TextFieldEvent : UIElemementValueChangedEvent<TextField, string>
    {
    }
}