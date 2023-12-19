/*
 USTRAP
 DATA CLASS
 UI ELEMENT VALUE CHANGED EVENT
 v1.0
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UStrap.Data
{
    /// <summary>
    /// Base class used to register value changed events to Visual Elements.
    /// </summary>
    /// <typeparam name="Element">Type of VisualElement</typeparam>
    /// <typeparam name="T">Type of value the visual element supports.</typeparam>
    public abstract class UIElemementValueChangedEvent<Element, T> : UIElementClickEvent<Element>
    where Element : VisualElement, INotifyValueChanged<T>
    {
        public UnityEvent<T> _OnValueChanged;

        public override void RegisterEvents(Element slider)
        {
            base.RegisterEvents(slider);
            RegisterEvents(slider,
            (e) => {
                _OnValueChanged?.Invoke(e.newValue); //event
                OnValueChanged(slider, e); //engine
            });
        }

        /// <summary>
        /// Registers callback events to the given Slider.
        /// </summary>
        /// <param name="e">Reference to the element.</param>
        /// <param name="valueChanged">The new value event</param>
        public void RegisterEvents(
            Element e,
            EventCallback<ChangeEvent<T>> valueChanged
        )
        {
            if (e == null)

            e.RegisterValueChangedCallback(valueChanged);
        }

        #region ENGINE
        /// <summary>
        /// OnValueChanged is called when the value of the element is changed.
        /// </summary>
        /// <param name="element">The elemet who's value was changed.</param>
        /// <param name="e">Change Value Event</param>
        protected virtual void OnValueChanged(Element element, ChangeEvent<T> e) { }
        #endregion
    }
}