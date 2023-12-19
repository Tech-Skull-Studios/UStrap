﻿/*
 USTRAP
 DATA CLASS
 FOLDOUT EVENT
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Wrapper for foldout events in the inspector.
    /// </summary>
    [Serializable]
    public class FoldoutEvent : UIElemementValueChangedEvent<Foldout, bool>
    {
    }
}