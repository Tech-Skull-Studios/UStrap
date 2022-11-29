/*
 USTRAP
 DATA CLASS
 UI ELEMENT CLICK EVENT
 v1.0
 LAST EDITED: TUESDAY NOVEMBER 29, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Base class used to register mouse events to Visual Elements.
    /// </summary>
    /// <typeparam name="Element">Type of VisualElement</typeparam>
    [Serializable]
    public abstract class UIElementClickEvent<Element> where Element : VisualElement
    {
        public string QueryTag;
        public UnityEvent<Element> OnClick;
        public UnityEvent<Element> OnRelease;
        public UnityEvent<Element> OnMouseEnter;
        public UnityEvent<Element> OnMouseExit;

        /// <summary>
        /// Registers the inspector events to the element given.
        /// </summary>
        /// <param name="element">The element to register the events to.</param>
        public void RegisterEvents(Element element)
        {
            if (element == null)
                throw new NullReferenceException("element");

            RegisterEvents(element,
                (e) => {
                    OnClick?.Invoke(element); //event
                    OnMouseClicked(element, e); //engine
                },
                (e) => {
                    OnRelease?.Invoke(element); //event
                    OnMouseReleased(element, e); //engine
                },
                (e) => {
                    OnMouseEnter?.Invoke(element); //event
                    OnMouseHover(element, e); //engine
                },
                (e) => {
                    OnMouseExit?.Invoke(element); //event
                    OnMouseExited(element, e); //engine
                }
            );
        }

        /// <summary>
        /// Registers callback events to the given CallbackEventHandler.
        /// </summary>
        /// <param name="e">Event Handler</param>
        /// <param name="mouseDown">Mouse Clicked Event</param>
        /// <param name="mouseUp">Mouse Released Event</param>
        /// <param name="mouseHoverEnter">Mouse Hover Event</param>
        /// <param name="mouseHoverExit">Mouse Exited Event</param>
        public void RegisterEvents(
            CallbackEventHandler e,
            EventCallback<MouseDownEvent> mouseDown = null,
            EventCallback<MouseUpEvent> mouseUp = null,
            EventCallback<MouseOverEvent> mouseHoverEnter = null,
            EventCallback<MouseOutEvent> mouseHoverExit = null
        )
        {
            if (e == null)
                throw new NullReferenceException("e");

            if(mouseDown != null)
                e.RegisterCallback(mouseDown);
            if(mouseUp != null)
                e.RegisterCallback(mouseUp);
            if(mouseHoverEnter != null)
                e.RegisterCallback(mouseHoverEnter);
            if(mouseHoverExit != null)
                e.RegisterCallback(mouseHoverExit);
        }

        #region ENGINE

        /// <summary>
        /// OnMouseClicked is called when the mouse is clicked on the element.
        /// </summary>
        /// <param name="element">The element that was clicked.</param>
        /// <param name="e">Mouse Clicked Event</param>
        protected virtual void OnMouseClicked(Element element, MouseDownEvent e) { }

        /// <summary>
        /// OnMouseReleased is called when the mouse is released on the element.
        /// </summary>
        /// <param name="element">The element that was released.</param>
        /// <param name="e">Mouse Released Event</param>
        protected virtual void OnMouseReleased(Element element, MouseUpEvent e) { }

        /// <summary>
        /// OnMouseHover is called when the mouse hovers over an element.
        /// </summary>
        /// <param name="element">The element that was hovered over.</param>
        /// <param name="e">Mouse Hover Event</param>
        protected virtual void OnMouseHover(Element element, MouseOverEvent e) { }

        /// <summary>
        /// OnMouseExited is called when the mouse existed the element.
        /// </summary>
        /// <param name="element">The element that was exited.</param>
        /// <param name="e">Mouse Leave Event</param>
        protected virtual void OnMouseExited(Element element, MouseOutEvent e) { }

        #endregion
    }
}