/*
 USTRAP
 EXTENSION CLASS
 EXTENDED VISUAL ELEMENT
 v1.0
 LAST EDITED: SUNDAY NOVEMBER 27, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using UnityEngine.UIElements;

namespace UStrap.Extensions
{
    public static class ExtendedVisualElement
    {
        /// <summary>
        /// Gets the root object of a VisualElement.
        /// </summary>
        /// <param name="element">Reference to the visual element.</param>
        /// <returns>Root of the element</returns>
        public static VisualElement GetRoot(this VisualElement element)
        {
            while (element != null && element.parent != null)
                element = element.parent;

            return element;
        }
    }
}