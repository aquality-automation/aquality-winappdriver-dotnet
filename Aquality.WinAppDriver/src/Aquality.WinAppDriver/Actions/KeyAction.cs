using System;
using System.Collections.Generic;

namespace Aquality.WinAppDriver.Actions
{
    public class KeyAction
    {
        public TimeSpan? Pause { get; set; }

        public string Text { get; set; }

        public int? VirtualKeyCode { get; set; }  

        public bool? Down {  get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            var result = new Dictionary<string, object>();
            if (Pause != null)
            {
                result.Add("pause", Pause?.TotalMilliseconds);
            }
            if (Text != null)
            {
                result.Add("text", Text);
            }
            if (VirtualKeyCode != null)
            {
                result.Add("virtualKeyCode", VirtualKeyCode);
            }
            if (Down != null)
            {
                result.Add("down", Down);
            }
            return result;
        }
    }
}
