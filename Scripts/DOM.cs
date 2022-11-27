using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UStrap
{
    public static class DOM
    {
        /// <summary>
        /// Opens a new Document and returns the Tree
        /// </summary>
        /// <param name="root">Root Document</param>
        /// <param name="document">Tree to Attach to the Root</param>
        public static void OpenDocument(UIDocument root, Action<TemplateContainer> document)
        {
            var Tree = root.visualTreeAsset.CloneTree();
            document.Invoke(Tree);
            Update(root, Tree);
        }

        /// <summary>
        /// Replaces the current root tree with the new template container
        /// </summary>
        /// <param name="root">Root Document</param>
        /// <param name="Tree">Tree to Attach to the Root</param>
        public static void Update(UIDocument root, TemplateContainer Tree)
        {
            //Assign the DOM back to the root
            root.rootVisualElement.Clear();
            root.rootVisualElement.Add(Tree as VisualElement);
        }
    }
}