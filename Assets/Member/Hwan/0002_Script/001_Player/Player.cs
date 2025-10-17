using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statSO;
    [SerializeField] private PlayerInputSO inputSO;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private bool IsMoving;

    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.StatSO = statSO;

        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.StatSO = statSO;

        inputSO.OnMouseMove += playerMovement.SetMouseDeg;
        inputSO.OnSpaceDown += (performed) => IsMoving = performed;
        //inputSO.OnMouseClick += () => playerAttack.Attack(playerMovement.IsMoving);
    }

    private void Update()
    {
        
    }
}
