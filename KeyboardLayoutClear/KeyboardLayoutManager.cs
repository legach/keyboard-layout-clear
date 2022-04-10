using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutClear
{
    class KeyboardLayoutManager
    {
        [DllImport("user32.dll")] static extern bool UnloadKeyboardLayout(IntPtr hkl);
        public bool TryToUnloadLayout(int id)
        {
            try
            {
                var result = UnloadKeyboardLayout((IntPtr)id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("! Exception: " + ex.Message);
                return false;
            }
        }

        [DllImport("user32.dll")] static extern UInt32 GetKeyboardLayoutList(Int32 nBuff, IntPtr[] lpList);

        public IEnumerable<int> GetAllKeyboardLayout()
        {
            var count = GetKeyboardLayoutList(0, null);
            var keyboardLayoutIds = new IntPtr[count];
            GetKeyboardLayoutList(keyboardLayoutIds.Length, keyboardLayoutIds);

            foreach (var item in keyboardLayoutIds)
            {
                yield return item.ToInt32() & 0xFFFF;
            }
        }
    }
}
