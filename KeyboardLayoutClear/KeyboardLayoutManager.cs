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
                var languageNameId = item.ToInt32() & 0xFFFF;
                var languageName = languageNameId > 0
                    ? CultureInfo.GetCultureInfo(languageNameId).DisplayName
                    : "Unknown";

                var keyboardNameId = item.ToInt32() >> 16;
                var keyboardName = keyboardNameId > 0 
                    ? CultureInfo.GetCultureInfo(keyboardNameId).DisplayName 
                    : "Unknown";


                yield return new LayoutModel(item.ToInt64(), languageName, keyboardName);
            }
        }


    }
}
