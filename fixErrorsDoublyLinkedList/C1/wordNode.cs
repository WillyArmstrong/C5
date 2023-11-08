using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLList
{
    internal class wordNode
    {
        public string Word { get; set; }
        public int Length { get; set; }
        public wordNode Next { get; set; }
        public wordNode Prev { get; set; }

        public wordNode()
        {
            Next = null;
            Prev = null;
            Word = null;
        }
        public wordNode(string word)
        {
            Next = null;
            Prev = null;
            Word = null;
            this.Word = word;
            Length = word.Length;
        }


        public string ToPrint()
        {
            return Word.ToString();
        }

        public override string ToString()
        {
            return Word.ToString();
        }
    }


}
