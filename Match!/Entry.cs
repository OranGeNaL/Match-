using System;
using System.Collections.Generic;
using System.Text;

namespace Match_
{
    public class Entry
    {
        string text;
        int line;
        int point;

        public Entry(string t, int l, int p)
        {
            text = t;
            line = 0;
            point = p;
        }

    public override string ToString()
        {
            return string.Format("Текст: {0} | Строка: {1} | Символ: {2}", text, line, point);
        }
    }
}
