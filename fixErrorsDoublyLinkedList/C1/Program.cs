using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DBLList
{
    internal class Program
    {


        static wordDBLList wordList = new wordDBLList();
        static void Main(string[] args)
        {
            //  ChooseDirectory();
            GetDirectoryFixed();
            LookAtList();
            AddIn();         
            SearchFor();
            Peek();
            RemoveFirst();
            RemoveLast();
            RemoveSelectedWord();
            SearchFor();
            Peek();
            Continue();
        }
        #region File Load Operations
        private static void GetDirectoryFixed()
        {
            string folderPath = @"C:\Users\willy\Downloads\Word Files-20230402";
            DirectoryInfo directory = new DirectoryInfo(folderPath);

            List<FileInfo> files = directory.GetFiles().OrderBy(f => f.Length).ToList();

            foreach (FileInfo file in files)
            {
                string fileContent = File.ReadAllText(file.FullName);
                fileContent = fileContent.Replace("#", "");
                string[] words = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    wordList.AddToFront(word);
                }
            }
        }

        private static string ChooseDirectory()
        {
            Console.WriteLine("Please enther the Exact Directory you wish to Browse (must be EXACT)");
            string choosenFolderPath = Console.ReadLine();
            /*            if (Directory.Exists(choosenFolderPath))
                        {
                            Console.WriteLine("This Directory Doesnt Work");
                            return null;
                        }*/
            DirectoryInfo directory = new DirectoryInfo(choosenFolderPath);

            List<FileInfo> files = directory.GetFiles().OrderBy(f => f.Length).ToList();

            foreach (FileInfo file in files)
            {
                string fileContent = File.ReadAllText(file.FullName);
                fileContent = fileContent.Replace("#", "");
                string[] words = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    wordList.AddToFront(word);
                }
            }
            LookAtList();
            return choosenFolderPath;

        }

        #endregion

        #region Find Operations

        private static void SearchFor()
        {
            Console.WriteLine(wordList.Find("Sting"));
            Console.WriteLine(wordList.Find("William"));
            Console.WriteLine(wordList.Find("Johnwick"));
            Console.WriteLine(wordList.Find("Arnold"));
        }

        private static void Peek()
        {
            Console.Write(wordList.PeekWord());
        }

        #endregion

        #region Insert Operations

        private static void AddIn()
        {
            wordList.AddToFront("Sting");
            wordList.AddToRear("William");
            wordList.AddBefore("Johnwick", 400);
            wordList.AddAfter("Arnold", 400);
        }

        #endregion

        #region Delete Operations
        private static void RemoveFirst()
        {
            wordList.RemoveFront();
        }

        private static void RemoveLast()
        {
            wordList.RemoveBack();
        }

        private static void RemoveSelectedWord()
        {
            wordList.RemoveWord("Johnwick");
            wordList.RemoveWord("Arnold");
        }

        #endregion

        #region ToPrint Operations
        private static void LookAtList()
        {
            Console.WriteLine(wordList.ToPrintWord());
        }

        #endregion

        private static void Continue()
        {
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}