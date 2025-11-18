using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseAction", story: "[Target] [SharkMovement]", category: "Action", id: "03346906199e2cd3bc902231293d76c5")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<SharkMovement> SharkMovement;
    
    
    private Transform _target;
    private SharkMovement _movement;
    private GameObject _shark;

    protected override Status OnStart()
    {
        _target = Target.Value;
        _movement = SharkMovement.Value;
        _shark = _movement.gameObject;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Debug.Log("추격");
        _movement.RbCompo.linearVelocity = (_target.position - _shark.transform.position).normalized; 
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

