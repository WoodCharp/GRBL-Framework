using System;
using System.Windows.Forms;

namespace TestApp
{
    public static class Invoker
    {
        public static void Invoke(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                    control.Invoke(new MethodInvoker(action), null);
                else
                    action.Invoke();
            }
            catch { }
        }
    }
}
