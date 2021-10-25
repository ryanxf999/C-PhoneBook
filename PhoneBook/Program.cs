/*
 This program was created by Ryan Fontenot. 
 The PhoneBook program is designed to allow users to save, list, and delete contacts they input. 
 My interpretation of this program was to program regular expression and use it as an input validator. 
 This will restrict what users input and only allowing what is needed and nothing else. 

 Addition code that was used:
 https://tinyurl.com/ycanzt8v
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace PhoneBook
{
    class PhoneBook1
    {
         public void Start()
        {
            //building the intro
            Console.WriteLine("\n============================================");
            Console.WriteLine("         WELCOME TO THE DIRECTORY");
            Console.WriteLine("============================================");
            Console.WriteLine();
            Console.WriteLine("LIST - Displays all contacts");
            Console.WriteLine("ADD - input name and number into the directory");
            Console.WriteLine("DEL - Will remove contact from the directory\n");
            Console.Write("Please enter \"LIST\"|\"ADD\"|\"DEL\": ");
            var input = Console.ReadLine();

            CheckInputOption(input);


        }//end START

        //This method was will check the input from the user and sends the user to anothe
        private void CheckInputOption(string i)
        {
            var input = i;
            var add1 = "ADD";
            var del1 = "DEL";
            var list1 = "LIST";
            bool isTrue = true;

            string addDelList = "^(ADD|DEL|LIST)$";
            Regex RGX = new Regex(addDelList);

            //These are my REGEX strings that i used for checking names and phone numbers 
            string names1 = "^(?n:(?<lastname>(St\\.\\ )?(?-i:[A-Z]\'?\\w+?\\-?)+)(?<suffix>\\ (?i:([JS]R)|((X(X{1,2})?)?" +
                "((I((I{1,2})|V|X)?)|(V(I{0,3})))?)))?,((?<prefix>Dr|Prof|M(r?|(is)?)s)\\ )?(?<firstname>(?-i:[A-Z]\'?(\\w+?|\\.)\\ ??)" +
                "{1,2})?(\\ (?<mname>(?-i:[A-Z])(\'?\\w+?|\\.))){0,2})$";
            string names2 = "(?<FirstName>[A-Z]\\.?\\w*\\-?[A-Z]?\\w*)\\s?(?<MiddleName>[A-Z]\\w+|[A-Z]?\\.?)\\s(?<LastName>(?:[A-Z]" +
                "\\w{1,3}|St\\.\\s)?[A-Z]\\w+\\-?[A-Z]?\\w*)(?:,\\s|)$";
            string names3 = "(?n:(^(?(?![^,]+?,)((?<first>[A-Z][a-z]*?) )?((?<second>[A-Z][a-z]*?) )?((?<third>[A-Z][a-z]*?) )?)" +
                "(?<last>[A-Z](('|[a-z]{1,2})[A-Z])?[a-z]+))(?(?=,)" +
                "(, (?<first>[A-Z][a-z]*?))?( (?<second>[A-Z][a-z]*?))?( (?<third>[A-Z][a-z]*?))?)$)";
            string phoneNumber1 = "^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$";

            //if statements to move around the code
            if (add1 == input)
            {
                AddContacts();
            }
            if (del1 == input)
            {
                DeleteContacts();
            }
            if (list1 == input)
            {
                DisplayContacts();
            }
            else
                Console.WriteLine();
            Start();
        }
        //this  allows user to input contacts via phone number or name to get deleted using regex
        private void DeleteContacts()
        {
            Console.WriteLine();
            string userInput = "";
            string path = "contacts.txt";

            string badStuff = "^(?!.*:|.*<>|.*\\(\\)|.* CR |.* LF)[^&;$%\"]*$";
            Regex RGX = new Regex(badStuff);

            Console.Write("Enter name you or number you want to delete:");
            string selectedContact = Console.ReadLine();

            if (!RGX.IsMatch(selectedContact))
            {
                Console.Write("INVALID INPUT. RETURNING TO MAIN MENU. \n");
            }
            Console.WriteLine();
            var oldLines = System.IO.File.ReadAllLines(path);
            var newLines = oldLines.Where(line => !line.Contains(selectedContact));
            System.IO.File.WriteAllLines(path, newLines);
            FileStream obj = new FileStream(path, FileMode.Append);
            obj.Close();
            FileInfo fi = new FileInfo(path);
            using (StreamReader sr = fi.OpenText())
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    Console.WriteLine();
                }
                sr.Close();
            }
            FileStream obj1 = new FileStream(path, FileMode.Append);
            obj1.Close();
        }
        //DisplayContacts loops through the text document to display each record in the file.
        private void DisplayContacts()
        {
            Console.WriteLine("\n*****************************");
            FileInfo myFile = new FileInfo("contacts.txt");
            string input;

            using (StreamReader sr = myFile.OpenText())
            {
                string s = null;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(input = s);
                    Console.WriteLine();
                }
                sr.Close();
            }
            Console.WriteLine("*****************************\n");
        }
        //AdContacts method usesregex to accept users contact information and nothing else.
        private void AddContacts()
        {
            FileInfo myFile = new FileInfo("contacts.txt");
            string input;

            string badStuff = "^(?!.*:|.*<>|.*\\(\\)|.* CR |.* LF)[^&;$%\"]*$";
            Regex RGX = new Regex(badStuff);

            Console.Write("enter name and phone number: ");
            input = Console.ReadLine();

            if (!RGX.IsMatch(input))
            {
                Console.Write("INVALID INPUT. RETURNING TO MAIN MENU. ");
            }
            else
            {
                using (StreamWriter sw = myFile.AppendText())
                {
                    sw.WriteLine(input);
                    Console.WriteLine("\nCONTACT ADDED. RETURNING TO MAIN MENU.\n");
                }
            }
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            PhoneBook1 pb = new PhoneBook1();
            pb.Start();

        }

    }
}
