using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UStrap.Data;

namespace UStrap
{
    public enum QueryType
    {
        Name,
        Class
    }

    public class DOMManager : MonoBehaviour
    {
        public static TemplateContainer _DOM;
        public static UIDocument Document;

        [SerializeField] private UIDocument Root;  
        public List<ButtonClickEvent> ButtonEvents;

        public void Awake()
        {
            Document = Root;
            DOM.OpenDocument(Document, (document) => {
                //Debug.Log(document);
                _DOM = document; //(DOMContainer)(document as IBindable);
                //Debug.Log(_DOM);
                //Handle Button Events
                foreach (var _event in ButtonEvents)
                {
                    (_event.QueryType == QueryType.Name ?
                    _DOM.Q<Button>(_event.QueryTag) :
                    _DOM.Q<Button>(null, _event.QueryTag.Split('.'))).clicked += () =>
                    {
                        _event.OnClick.Invoke();
                    };
                }
            });

            RemoveComponent(QueryType.Name, "button-secondary");
        }

        /// <summary>
        /// Returns the first instance found of a component
        /// given its name or identifying class
        /// </summary>
        /// <typeparam name="T">Type of Visual Element to Return</typeparam>
        /// <param name="QueryType">Name or Class</param>
        /// <param name="QueryTag">Selector Tag</param>
        /// <returns></returns>
        public static T GetComponent<T>(QueryType QueryType, string QueryTag) where T : VisualElement
        {
            Debug.Log(_DOM);
            //Debug.Log(_DOM.Q<T>(null, "btn-secondary"));
            return QueryType == QueryType.Name ?
            _DOM.Q<T>(QueryTag) :
            _DOM.Q<T>(null, QueryTag.Split('.'));
        }

        //BROKEN!!!!
        //public IEnumerable<T> GetComponents<T>(QueryType QueryType, string QueryTag) where T : VisualElement
        //{
        //    return QueryType == QueryType.Name ?
        //    new List<T>() { GetComponent<T>(QueryType, QueryTag) } :
        //    GetComponentsFromClass<T>(QueryTag);
        //}
        //
        //private List<T> GetComponentsFromClass<T>(string QueryTag) where T : VisualElement
        //{
        //    var classes = QueryTag.Split('.');
        //    var elements = Traverse<T>(_DOM.).Where(x => x.GetClasses().Intersect(classes).Count() == classes.Length).ToList();
        //    return elements;
        //}
        //
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

        public static void RemoveComponent(QueryType QueryType, string QueryTag)
        {
            List<VisualElement> elementsToRemove = new List<VisualElement>()
            {
                QueryType == QueryType.Name ?
                _DOM.Q(QueryTag) :
                GetComponent<VisualElement>(QueryType, QueryTag) //TODO: get a list of multiple components to destroy for classes
            };

            foreach(var component in elementsToRemove)
            {
                if (component != null)
                {
                    var parent = component.parent;
                    if (parent != null)
                        parent.Remove(component);
        
                    DOM.Update(Document, _DOM);
                }
            }
        }
    }
}