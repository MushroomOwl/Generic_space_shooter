using System;

namespace Game
{
    public static class Commons
    {
        public static string IntToTextForUI(int value)
        {
            return "x " + value;
        }

        public static string RoundFloatToTextForUI(float value)
        {
            return "x " + (int)Math.Floor(value);
        }
    }
}
