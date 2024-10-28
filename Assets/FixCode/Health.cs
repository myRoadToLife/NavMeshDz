using UnityEngine;

namespace FixCode
{
    public class Health
    {

        public Health()
        {
            Value = MaxValue;
        }

        public float Value { get; private set; }
        public float MaxValue { get; private set; } = 100;

        public void Reduce(float value)
        {
            if (value < 0)
            {
                Debug.LogError("Value < 0");
                return;
            }

            Value -= value;

            if (Value < 0)
            {
                Value = 0;
            }

            Debug.Log(Value);
        }
    }
}

