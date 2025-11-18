using Member.Kimyongmin._02.Code.Boss.SO;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackAction", story: "[Target] [SharkData]", category: "Action", id: "70b0bccaee732523f1b9106a146519c7")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<SharkDataSO> SharkData;

    protected override Status OnStart()
    {
        Debug.Log("공격");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

