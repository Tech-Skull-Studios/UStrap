﻿/*
 USTRAP CLASS
 DOM
 v1.1
 LAST EDITED: WEDNESDAY SEPTEMBER 4, 2024
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.UIElements;

namespace UStrap
{
    /// <summary>
    ///     Controls DOM operations.
    /// </summary>
    public static class DOM
    {
        /// <summary>
        ///     Opens a new Document and returns the Tree
        /// </summary>
        /// <param name="root">Root Document</param>
        /// <param name="document">Tree to Attach to the Root</param>
        public static void OpenDocument(UIDocument root, Action<TemplateContainer> document)
        {
            var Tree = root.visualTreeAsset.CloneTree();
            document?.Invoke(Tree); //invoke callback
            Update(root, Tree);
        }

        /// <summary>
        ///     Replaces the current root tree with the new template container
        /// </summary>
        /// <param name="root">Root Document</param>
        /// <param name="Tree">Tree to Attach to the Root</param>
        public static void Update(UIDocument root, TemplateContainer Tree)
        {
            //Assign the DOM back to the root
            root.rootVisualElement.Clear();
            root.rootVisualElement.Add(Tree);
        }
    }
}