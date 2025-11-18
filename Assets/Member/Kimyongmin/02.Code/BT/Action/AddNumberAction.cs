using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AddNumber", story: "Add [Number] at [TargetNumber]", category: "Action", id: "bb445f0ff8f7d2e9555cddd0dd32f815")]
public partial class AddNumberAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Number;
    [SerializeReference] public BlackboardVariable<int> TargetNumber;

    protected override Status OnStart()
    {
        TargetNumber.Value += Number.Value;
        return Status.Success;
    }
}

