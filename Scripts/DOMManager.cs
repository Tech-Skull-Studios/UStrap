/*
 USTRAP CLASS
 DOM MANAGER
 v1.2
 LAST EDITED: MONDAY DECEMBER 19, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UStrap.Data;
using UStrap.Extensions;
using Cursor = UnityEngine.Cursor;

namespace UStrap
{
    /// <summary>
    /// Added to the scene hierarchy to assist with document management.
    /// </summary>
    [AddComponentMenu("UStrap/" + Filename)]
    public class DOMManager : MonoBehaviour
    {
        public const string Filename = "DOM Manager";

        #region VARIABLE DECLARATIONS

        //public static DOMManager Instance { get; private set; }
        public static TemplateContainer _DOM { get; private set; }
        public static UIDocument Document { get; private set; }
        public static bool IsDocumentLoaded => Document != null;

        [SerializeField] private bool OpensDocumentOnAwake = true;
        [SerializeField] private UIDocument Root;

        [Header("Cursor")]
        [SerializeField] private Texture2D NormalCursor;
        [SerializeField] private Vector2 NormalCursorHotspot;
        [SerializeField] private Texture2D HoverCursor;
        [SerializeField] private Vector2 HoverCursorHotspot;
        
        [Header("Events")]
        [SerializeField] private ButtonEvent[] ButtonEvents;
        [SerializeField] private ToggleEvent[] ToggleEvents;
        [SerializeField] private TextFieldEvent[] TextFieldEvents;
        [SerializeField] private SliderEvent[] SliderEvents;
        [SerializeField] private SliderEventInt[] IntSliderEvents;
        [SerializeField] private SliderEventMinMax[] MinMaxSliderEvents;
        [SerializeField] private LabelEvent[] LabelEvents;
        [SerializeField] private ScrollerEvent[] ScrollerEvents;
        [SerializeField] private FoldoutEvent[] FoldoutEvents;

        private VisualElement cursor;

        private void UpdateCursorPosition()
        {
            if (cursor == null) return;
            cursor.style.left = 0;
            cursor.style.top = 0;
        }

        #endregion

        #region SETUP

        private void Awake()
        {
            //Destroy the instance if the DOM Manager already exists.
            //if(Instance != null && Instance != this)
            //{
            //    Destroy(gameObject);
            //    return;
            //}
            
            //Instance = this;
            //DontDestroyOnLoad(gameObject);
            if (OpensDocumentOnAwake)
                OpenDocument(Root);
        }

        /// <summary>
        /// Opens a UIDocument replacing the existing root.
        /// </summary>
        /// <param name="Root">New root path of the document to open</param>
        public void OpenDocument(UIDocument Root)
        {
            Document = Root;
            DOM.OpenDocument(Document, (document) => {
                _DOM = document;
                SwapCursor(NormalCursor, NormalCursorHotspot);

                EventRegistrar();

                //RemoveElement("#container");
                HideElement("#secondary-button");
                //ShowElement("#secondary-button");
            });
        }

        private void EventRegistrar()
        {
            //Handle Button Events
            RegisterEventToElement(ButtonEvents);
            //Handle Toggle Events
            RegisterEventToElement(ToggleEvents);
            //Handle TextField Events
            RegisterEventToElement(TextFieldEvents);
            //Handle Slider Events
            RegisterEventToElement(SliderEvents);
            //Handle Int Slider Events
            RegisterEventToElement(IntSliderEvents);
            //Handle Min Max Slider Events
            RegisterEventToElement(MinMaxSliderEvents);
            //Handle Label Events
            RegisterEventToElement(LabelEvents);
            //Handle Scroller Events
            RegisterEventToElement(ScrollerEvents);
            //Handle Foldout Events
            RegisterEventToElement(FoldoutEvents);

            //foreach (var _event in ButtonEvents)
            //{
            //    var btn = GetElement<Button>(_event.queryTag);
            //    if (btn == null) continue; //Button was not found
            //                               //_event.RegisterEvents(btn);
            //
            //    //btn.clicked += () =>
            //    //{
            //    //    _event.OnClick?.Invoke(btn);
            //    //};
            //    //btn.RegisterCallback<MouseOverEvent>((type) =>
            //    //{
            //    //    _event.OnMouseEnter?.Invoke(btn);
            //    //    SwapCursor(HoverCursor, HoverCursorHotspot);
            //    //});
            //    //btn.RegisterCallback<MouseOutEvent>((type) =>
            //    //{
            //    //    _event.OnMouseExit?.Invoke(btn);
            //    //    SwapCursor(NormalCursor, NormalCursorHotspot);
            //    //});
            //}
        }

        private void RegisterEventToElement<Type>(UIElementClickEvent<Type>[] events)
        where Type: VisualElement
        {
            foreach (var _event in events)
            {
                var element = GetElement<Type>(_event.queryTag);
                if (element == null) continue; //element was not found
                _event.RegisterEvents(element);

                if (element is Button
                    || element is TextField
                    || element is Slider
                    || element is SliderInt
                    || element is MinMaxSlider
                    || element is Scroller
                    || element is Toggle
                )
                {
                    element.RegisterCallback<MouseOverEvent>(_ => SwapCursor(HoverCursor, HoverCursorHotspot));
                    element.RegisterCallback<MouseOutEvent>(_ => SwapCursor(NormalCursor, NormalCursorHotspot));
                }
            }
        }

        /// <summary>
        /// Closes the currently opened document.
        /// </summary>
        public void CloseDocument()
        {
            if (!IsDocumentLoaded) return;
            Document.rootVisualElement.Clear();
            Document = null;
            _DOM = null;
        }

        #endregion

        #region CURSOR

        private void SwapCursor(Texture2D cursor, Vector2 hotspot)
        {
            if (cursor == null) return;
            Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
        }

        #endregion

        #region GET ELEMENTS

        /// <summary>
        /// Returns the first instance found of a component
        /// given its name or identifying class
        /// </summary>
        /// <typeparam name="T">Type of Visual Element to Return</typeparam>
        /// <param name="QueryTag">Selector Tag</param>
        /// <returns>The component in the document if it exists</returns>
        public static T GetElement<T>(string QueryTag) where T : VisualElement
        {
            if (!QueryTag.ValidateQueryString()) return null;
            return QueryTag.IsID() ?
            _DOM.Q<T>(QueryTag.TrimAndErase("#")) :
            _DOM.Q<T>(null, QueryTag.TrimAndSplit('.'));
        }

        /// <summary>
        /// Returns all found instances of a class and the first
        /// found instance of an ID as a list of VisualElements.
        /// </summary>
        /// <typeparam name="T">Type of Visual Element to Return</typeparam>
        /// <param name="QueryTag">Selector Tag</param>
        /// <returns>All found elements in the open document that match the selectors</returns>
        public static List<T> GetElements<T>(string QueryTag) where T : VisualElement
        {
            if (!QueryTag.ValidateQueryString()) return null;
            return new List<T>(
                QueryTag.IsID() ?
                new List<T>() { GetElement<T>(QueryTag) } :
                GetElementsFromClass<T>(QueryTag)
            );
        }
        
        //BROKEN!!!!
        private static List<T> GetElementsFromClass<T>(string QueryTag) where T : VisualElement
        {
            var classes = QueryTag.GetClassList();
            if (classes.Length == 0) return new List<T>();
            return null;
            //var elements = Traverse<T>(_DOM.).Where(x => x.GetClasses().Intersect(classes).Count() == classes.Length).ToList();
            //return elements;
        }

        //public static IEnumerable<T> Traverse<T>(this T root) where T : VisualElement
        //{
        //    var stack = new Stack<T>();
        //    stack.Push(root);
        //    while (stack.Count > 0)
        //    {
        //        var current = stack.Pop();
        //        yield return current;
        //        foreach (var child in current.Children())
        //            stack.Push((T)child);
        //    }
        //}

        #endregion

        #region DELETE ELEMENTS

        /// <summary>
        /// Removes an element from the currently opened document.
        /// </summary>
        /// <param name="QueryTag">Selector Tag</param>
        public static void RemoveElement(string QueryTag)
        {
            if (!QueryTag.ValidateQueryString()) return;
            List<VisualElement> elementsToRemove = new List<VisualElement>(
                QueryTag.IsID() ?
                new List<VisualElement>() { _DOM.Q(QueryTag) } :
                GetElements<VisualElement>(QueryTag)
            );

            foreach(var element in elementsToRemove)
            {
                if (element != null)
                {
                    var parent = element.parent;
                    if (parent != null)
                        parent.Remove(element);
        
                    DOM.Update(Document, _DOM);
                }
            }
        }

        #endregion

        #region HIDE/SHOW ELEMENTS

        /// <summary>
        /// Hides an element given its selector tag.
        /// </summary>
        /// <param name="QueryTag">Selector Tag</param>
        public static void HideElement(string QueryTag)
        {
            if (!QueryTag.ValidateQueryString()) return;
            List<VisualElement> elementsToHide = new List<VisualElement>(
                QueryTag.IsID() ?
                new List<VisualElement>() { _DOM.Q(QueryTag) } :
                GetElements<VisualElement>(QueryTag)
            );

            foreach(var element in elementsToHide)
            {
                if(element != null)
                    element.AddToClassList("hidden");
            }
        }

        /// <summary>
        /// Shows an element given its selector tag.
        /// </summary>
        /// <param name="QueryTag"></param>
        public static void ShowElement(string QueryTag)
        {
            List<VisualElement> elementsToShow = new List<VisualElement>(
                QueryTag.IsID() ?
                new List<VisualElement>() { _DOM.Q(QueryTag) } :
                GetElements<VisualElement>(QueryTag)
            );

            foreach (var element in elementsToShow)
            {
                if(element != null)
                    element.RemoveFromClassList("hidden");
            }
        }

        #endregion
    }
}