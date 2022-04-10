using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutClear
{
    class LayoutModel
    {
        public long Id { get; private set; }
        public string LanguageName { get; private set; }
        public string KeyboardName { get; private set; }

        public LayoutModel(long id, string languageName, string keyboardName)
        {
            Id = id;
            LanguageName = languageName;
            KeyboardName = keyboardName;
        }
    }
}
