/*
                                            Tasks:

    - Створити масив на N елементів, де N вказується з консольного рядка.
    - Заповнити його випадковими числами від 1 до 26 включно.
    - Створити 2 масива, де в 1 масиві будуть значення лише парних значень, а в другому непарних.
    - Замінити числа в 1 і 2 масиві на букви англійського алфавіту. Значення клітинок цих масивів
        дорівнюють порядковому номеру кожної букви в алфавіті.
    - Якщо ж буква є одній із списку (a, e, i, d, h, j) то вона має бути у верхньому регістрі.
    - Вивести на екран результат того, в якому з масивів буде більше букв у верхньому регістрі.
    - Вивести обидва масиви на екран. Кожен з масивів має бути виведений 1 рядком,
        де його значення будуть розділені пропуском.
 */

namespace NXS_Lesson4;

internal class Program
{
    public static void Main(string[] args)
    {
        int[] arr = new int[GetUserSize()];
        FillArray(arr);

        (int evenLength, int oddLength) = FindEvenOddElements(arr);

        // both arrays are object[] cuz we need to change their types (int -> char)
        object[] evenArr = new object[evenLength];
        object[] oddArr = new object[oddLength];

        for (int i = 0, e = 0, o = 0; i < arr.Length; i++)
        {
            if (arr[i] % 2 == 0)
            {
                evenArr[e++] = arr[i];
            }
            else
            {
                oddArr[o++] = arr[i];
            }
        }

        // count number of letters in uppercase in both arrays
        ConvertIntoCharArray(evenArr, oddArr, out int evenUpperCaseNumber, out int oddUpperCaseNumber);

        PrintBiggestUpperCaseArray(evenUpperCaseNumber, oddUpperCaseNumber);

        PrintArray("even", evenArr);
        PrintArray("odd ", oddArr);

        Console.ReadKey();
    }

    // Find out how big must be the array from user's prospective
    private static int GetUserSize()
    {
        int size = 0;

        while (true)
        {
            Console.Write("Enter array length: ");
            size = Convert.ToInt32(Console.ReadLine());

            if (size <= 1)
            {
                Console.WriteLine("Length must be at least 1 character long");
            }
            else
            {
                break;
            }
        }

        return size;
    }

    // Fill the array with random numbers (1..26)
    private static void FillArray(int[] arr)
    {
        Random rand = new Random();

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = rand.Next(1, 27); // 1..26 -> including 26
        }
    }

    // Find how many even/odd numbers are in the array beforehand to prevent resizing derivative arrays
    private static (int even, int odd) FindEvenOddElements(int[] arr)
    {
        int even = 0;
        int odd = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] % 2 == 0)
            {
                ++even;
            }
            else
            {
                ++odd;
            }
        }

        return (even, odd);
    }

    // Create char[] that consists of letters from the English alphabet (a..z)
    private static char[] GetEnglishAlphabetArray()
    {
        char[] arr = new char[26]; // number of letters in the English alphabet

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = Convert.ToChar(i + 97); // 'a' -> 97

            switch (arr[i])
            {
                case 'a':
                case 'e':
                case 'i':
                case 'd':
                case 'h':
                case 'j':
                    arr[i] = Convert.ToChar(i + 65); // 'A' -> 65
                    break;
            }
        }

        return arr;
    }

    // Convert int elements in object array into char AND
    // find how many letters in uppercase are in both arrays
    private static void ConvertIntoCharArray(object[] evenArr, object[] oddArr, out int evenUpperCaseNumber, out int oddUpperCaseNumber)
    {
        char[] engLettersArr = GetEnglishAlphabetArray();

        evenUpperCaseNumber = 0;
        oddUpperCaseNumber = 0;

        for (int i = 0; i < evenArr.Length; i++)
        {
            // cuz arr[0] = 'A' and arr[25] = 'z' but num  can be only 1..26
            char ch = engLettersArr[(int)evenArr[i] - 1];

            if (char.IsUpper(ch))
            {
                ++evenUpperCaseNumber;
            }

            evenArr[i] = ch;
        }

        for (int i = 0; i < oddArr.Length; i++)
        {
            // cuz arr[0] = 'A' and arr[25] = 'z' but num  can be only 1..26
            char ch = engLettersArr[(int)oddArr[i] - 1];

            if (char.IsUpper(ch))
            {
                ++oddUpperCaseNumber;
            }

            oddArr[i] = ch;
        }
    }

    // Find which array consists of the greater part of uppercase letters and print it
    private static void PrintBiggestUpperCaseArray(int evenUpperCaseNumber, int oddUpperCaseNumber)
    {
        Console.WriteLine();

        if (evenUpperCaseNumber > oddUpperCaseNumber)
        {
            Console.WriteLine("Array with only even numbers (-> letters) contains " +
                $"the greater part of letters in uppercase: {evenUpperCaseNumber}");
        }
        else if (evenUpperCaseNumber < oddUpperCaseNumber)
        {
            Console.WriteLine("Array with only odd numbers (-> letters) contains " +
                $"the greater part of letters in uppercase: {oddUpperCaseNumber}");
        }
        else if (evenUpperCaseNumber == oddUpperCaseNumber && evenUpperCaseNumber != 0)
        {
            Console.WriteLine("Both arrays contain " +
                $"the same number of letters in uppercase: {evenUpperCaseNumber}");
        }
        else
        {
            Console.WriteLine("None of arrays contain letters in uppercase");
        }

        Console.WriteLine();
    }

    // Print elements of the array
    private static void PrintArray(string arrTag, object[] arr)
    {
        Console.Write($"Array with only {arrTag} numbers (-> letters): ");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }

        Console.WriteLine();
    }
}