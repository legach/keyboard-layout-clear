using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace KeyboardLayoutClear
{
    class KeyboardLayoutManager
    {
        [DllImport("user32.dll")] static extern bool UnloadKeyboardLayout(IntPtr hkl);
        public bool TryToUnloadLayout(long id)
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

        public IEnumerable<LayoutModel> GetAllKeyboardLayout()
        {
            var count = GetKeyboardLayoutList(0, null);
            var keyboardLayoutIds = new IntPtr[count];
            GetKeyboardLayoutList(keyboardLayoutIds.Length, keyboardLayoutIds);

            foreach (var item in keyboardLayoutIds)
            {
                yield return new LayoutModel(item.ToInt64(),
                    CultureInfo.GetCultureInfo(item.ToInt32() & 0xFFFF).DisplayName,
                    CultureInfo.GetCultureInfo(item.ToInt32() >> 16).DisplayName);
            }
        }


    }
}
