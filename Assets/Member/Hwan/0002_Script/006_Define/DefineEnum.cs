public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Flip,
    ZeroStamina,
    WaitForAttack,
    Hit
}

public enum PlayerEyeState
{
    None,
    Idle,
    Attack,
    Dead,
    Blink,
    Hit
}

public enum PlayerMovementType
{
    Swim,
    Dash,
    Flip,
    Rotate
}

public enum PlayerAttackType
{
    Dash,
    Flip
}

public enum PlayerMoveType
{
    Swim,
    Dash
}

public enum PlayerSkillType
{
    Dash,
    Flip,
    IndiaInk
}

public enum UIType
{
    None,
    GaugeUI,
    DistanceBarUI,
    TutorialUI,
    FadeUI,
    CounterUI
}