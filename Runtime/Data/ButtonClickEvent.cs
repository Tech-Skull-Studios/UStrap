using System;
using UnityEngine.Events;

namespace UStrap.Data
{
    [Serializable]
    public struct ButtonClickEvent
    {
        public QueryType QueryType;
        public string QueryTag;
        public UnityEvent OnClick;
    }
}