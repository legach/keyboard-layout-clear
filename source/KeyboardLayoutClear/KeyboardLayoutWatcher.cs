using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyboardLayoutClear
{
    public delegate void KeyboardLayoutChanged(int oldCultureInfo, int newCultureInfo);

    class KeyboardLayoutWatcher : IDisposable
    {
        private readonly Timer _timer;
        private int _currentLayout = 1033;


        public KeyboardLayoutChanged KeyboardLayoutChanged;

        public KeyboardLayoutWatcher()
        {
            _timer = new Timer(new TimerCallback(CheckKeyboardLayout), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
        [DllImport("user32.dll")] static extern IntPtr GetKeyboardLayout(uint thread);
        public int GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = GetForegroundWindow();
                uint foregroundProcess = GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
                int keyboardLayout = GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;

                if (keyboardLayout == 0)
                {
                    // something has gone wrong - just assume English
                    keyboardLayout = 1033;
                }
                return keyboardLayout;
            }
            catch (Exception ex)
            {
                Console.WriteLine("! Exception: " + ex.Message);
                // if something goes wrong - just assume English
                return 1033;
            }
        }

        private void CheckKeyboardLayout(object sender)
        {
            var layout = GetCurrentKeyboardLayout();
            if (_currentLayout != layout && KeyboardLayoutChanged != null)
            {
                KeyboardLayoutChanged(_currentLayout, layout);
                _currentLayout = layout;
            }

        }

        private void ReleaseUnmanagedResources()
        {
            _timer.Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~KeyboardLayoutWatcher()
        {
            ReleaseUnmanagedResources();
        }
    }
}
