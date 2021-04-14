using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpUISelenium.ServiceImplimentation
{
    public interface IUtilServices
    {
        string GenerateOneRandomString();
        string GenerateRandomNumber(int start, int end);
        int GetOddNumberInRange(int start, int end);
        bool StringValidation(string str1, string str2, STRINGCHECK str);
        void LauchApp(string url);
        string createDirectory(string Path);
        string Screenshot(string fileName);
        string getTimeStampAsString();

    }

    public enum STRINGCHECK
    {
        EQL, CONT
    }
    public enum DROPDOWNSELECTION
    {
        VALUE, INDEX
    }
}
