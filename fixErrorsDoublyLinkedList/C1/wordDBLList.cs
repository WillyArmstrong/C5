using System;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace DBLList
{
    internal class wordDBLList
    {
        #region HeadTailCurrentCounter
        public wordNode Head { get; private set; }
        public wordNode Tail { get; private set; }

        public wordNode Current { get; set; }

        public int Counter { get; set; }

        public wordDBLList()
        {
            Head = null;
            Tail = null;
            Current = null;
            Counter = 0;
        }

        #endregion

        #region insert operations

        private void InsertAtRear(wordNode wNode)
        {
            if (Tail == null)
            {
                Head = wNode;
                Tail = wNode;
            }
            else
            {
                wNode.Prev = Tail;
                Tail.Next = wNode;
                Tail = wNode;
            }
            Counter++;
        }

        private void InsertAtFront(wordNode wNode)
        {
            if (Head == null)
            {
                Head = wNode;
                Tail = wNode;
            }
            else
            {
                wNode.Next = Head;
                Head.Prev = wNode;
                Head = wNode;
                Current = wNode;
            }
            Counter++;
        }

        private bool InsertBefore(wordNode targetWord, wordNode newWord)
        {
            bool inserted = false;
            if (Head == null)
            {
                return inserted;
            }
            if (targetWord.Word == Head.Word)
            {
                InsertAtFront(newWord);
                inserted = true;
            }
            else
            {
                Current = Head;

                while (Current != null && !inserted)
                {
                    if (Current.Word == targetWord.Word)
                    {
                        newWord.Next = Current;
                        newWord.Prev = Current.Prev;
                        Current.Prev.Next = newWord;
                        Current.Prev = newWord;
                        inserted = true;
                        Counter++;
                    }
                    else
                    {
                        Current = Current.Next;
                    }
                }
            }
            return inserted;
        }


        private bool InsertAfter(wordNode targetWord, wordNode newWord)
        {
            bool inserted = false;
            if (Head == null)
            {
                return inserted;
            }

            Current = Head;

            while (Current != null && !inserted)
            {
                if (Current.Word == targetWord.Word)
                {
                    if (Current == Tail)
                    {
                        InsertAtRear(newWord);
                    }
                    else
                    {
                        newWord.Next = Current.Next;
                        newWord.Prev = Current;
                        newWord.Next.Prev = newWord;
                        Current.Next = newWord;
                        Current = newWord;
                    }
                    inserted = true;
                    Counter++;
                }
                else
                {
                    Current = Current.Next;
                }
            }
            return inserted;
        }

        public string AddAfter(string word, int target)
        {
            wordNode wNode = new wordNode(word.ToLower());
            wordNode targetWord = Head;
            int pos = 1;


            while (targetWord != null && pos < target)
            {
                targetWord = targetWord.Next;
                pos++;
            }

            if (targetWord != null)
            {
                if (InsertAfter(targetWord, wNode))
                {
                    return "Target: " + wNode.ToString() + ", Word was added after position: " + pos.ToString() + "\n";
                }
                else
                {
                    return "Target: " + targetWord.ToPrint() + " NOT found, WORD: " + wNode.ToPrint();
                }
            }
            else
            {
                return "Target position " + target + " not found, WORD: " + wNode.ToPrint();
            }
        }

        public string AddBefore(string word, int target)
        {
            wordNode wNode = new wordNode(word.ToLower());
            wordNode targetWord = Head;
            int pos = 1;

            while (targetWord != null && pos < target)
            {
                targetWord = targetWord.Next;
                pos++;
            }

            if (targetWord != null)
            {
                if (InsertBefore(targetWord, wNode))
                {
                    return "Target: " + wNode.ToString() + ", Word was found at position: " + pos.ToString() + "\n";
                }
                else
                {
                    return "Target: " + targetWord.ToPrint() + "NOT found, WORD: " + wNode.ToPrint(); ;

                }
            }
            else
            {
                return "Target position" + target + "not found, WORD: " + wNode.ToPrint();
            }
        }

        public void AddToRear(string word)
        {
            wordNode temp = new wordNode(word.ToLower());
            InsertAtRear(temp);
        }

        public void AddToFront(string word)
        {
            wordNode temp = new wordNode(word.ToLower());
            InsertAtFront(temp);
        }

        #endregion

        #region search operations

        private wordNode GetTop()
        {
            if (Current == null)
            {
                return null;
            }
            else
            {
                return Current;
            }
        }
        public string PeekWord()
        {
            wordNode nodeToPeek = GetTop();
            if (nodeToPeek != null)
            {
                return "The word " + nodeToPeek.Word + " is at the top of the list\n\n";
            }
            else
            {
                return "List is empty";
            }
        }
        public string Find(string Word)
        {
            int pos = 1;
            wordNode current = Head;
            Word = Word.ToLower();

            while (current != null)
            {
                if (current.Word == Word)
                {
                    return "Target: " + Word.ToString() + " , Length " + current.Length + ", Word was found at position: " + pos.ToString() + "\n";
                }
                current = current.Next;
                pos++;
            }

            return "Target: " + Word.ToString() + ", Word not found, OR list empty" + "\n";
        }

        #endregion

        #region delete operations
        private wordNode DeleteAtFront()
        {
            if (Head == null)
            {
                return null;
            }

            else
            {
                wordNode wordToRemove = new wordNode();
                wordToRemove = Head;

                Head = Head.Next;
                Head.Prev = Head;
                Counter--;
                return wordToRemove;
            }
        }

        private wordNode DeleteAtBack()
        {
            if (Head == null)
            {
                return null;
            }
            else
            {
                wordNode wordToRemove = new wordNode();
                wordToRemove = Tail;

                Tail = Tail.Prev;
                Tail.Next = null;
                Current = Tail;
                Counter--;
                return wordToRemove;
            }
        }

        private wordNode DeleteWord(wordNode wordToDelete)
        {
            wordNode wordToRemove = null;
            if (Head == null)
            {
                wordToRemove = null;
            }
            else if (Head.Word == wordToDelete.Word)
            {
                wordToRemove = Head;
                DeleteAtFront();
            }
            else if (Tail.Word == wordToDelete.Word)
            {
                wordToRemove = Tail;
                DeleteAtBack();
            }
            else
            {
                Current = Head;
                bool deleted = false;
                while (Current != null && !deleted)
                {
                    if (Current.Word == wordToDelete.Word)
                    {
                        wordToRemove = Current;
                        Current.Next.Prev = Current.Prev;
                        Current.Prev = Current.Next;
                        deleted = true;
                        Counter--;
                    }
                    Current = Current.Next;
                }
            }
            return wordToRemove;
        }

        public string RemoveFront()
        {
            wordNode wordToRemove = DeleteAtFront();
            if (wordToRemove != null)
            {
                return "Found, WORD: " + wordToRemove.ToString() + " removed";
            }
            else
            {
                return "Not found, or List Empty";
            }
        }

        public string RemoveBack()
        {
            wordNode wordToRemove = new wordNode();
            wordToRemove = DeleteAtBack();
            if (wordToRemove != null)
            {
                return "Found, NODE: " + wordToRemove.ToString() + " removed";
            }
            else
            {
                return " NOT found, or List Empty";
            }
        }

        public string RemoveWord(string word)
        {
            wordNode wordToRemove = new wordNode(word.ToLower());
            wordNode TargetWord = wordToRemove;
            wordToRemove = DeleteWord(wordToRemove);
            if (wordToRemove != null)
            {
                return "Found, NODE: " + TargetWord.ToString() + " removed";
            }
            else
            {
                return "Target " + TargetWord.ToString() + "Not found, list is empty";
            }
        }

        #endregion

        #region print operations

        public string ToPrintWord()
        {
            StringBuilder sb = new StringBuilder();
            wordNode currentWord = Head;
            if (Head == null)
            {
                sb.Append("List is empty \n");
            }
            else
            {
                wordNode temp = null;
                temp = Head;
                int pos = 1;
                while (temp != null)
                {
                    sb.Append("Word " + pos + ": " + temp.ToPrint() + ", Length: " + currentWord.Length + "\n");
                    currentWord = currentWord.Next;
                    temp = temp.Next;
                    pos++;
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            wordNode currentWord = Head;


            if (Head == null)
            {
                sb.Append("List is empty\n");
                return sb.ToString();
            }
            else
            {
                Current = Head;
                int pos = 1;
                while (Current != null)
                {
                    sb.Append("Word: " + currentWord.Word + ", Length: " + currentWord.Length);
                    currentWord = currentWord.Next;
                    Current = Current.Next;
                    pos++;
                }
            }
            return base.ToString();

        }

        #endregion

    }


}
