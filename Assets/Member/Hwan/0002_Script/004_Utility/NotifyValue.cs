using System;

public class NotifyValue<T>
{
    private T value;

    public event Action<T> OnValueChange;

    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            if (!Equals(value, this.value))
            {
                this.value = value;
                OnValueChange?.Invoke(this.value);
            }
        }
    }

    public NotifyValue(T initialValue = default)
    {
        this.value = initialValue;
    }
}
