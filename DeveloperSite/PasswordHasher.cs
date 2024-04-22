using System;
using System.Security.Cryptography;
using System.Text;

public class HashHelper
{
    public static string HashString(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Преобразование входной строки в массив байтов
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Хэширование массива байтов
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Преобразование хэша в строку шестнадцатеричного формата
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
