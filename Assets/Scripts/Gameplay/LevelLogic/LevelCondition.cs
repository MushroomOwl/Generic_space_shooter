using UnityEngine;
using UnityEngine.Events;

public class LevelCondition : MonoBehaviour
{
    public virtual bool Fulfilled { get; }

    protected UnityEvent _ConditionValueChanged = new UnityEvent();
    public UnityEvent ConditionValueChanged => _ConditionValueChanged;
}
