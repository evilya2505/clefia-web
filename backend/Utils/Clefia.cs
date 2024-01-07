using System;
using System.Text;
using System.Text.RegularExpressions;

public static class CLEFIACipherTest
{
    public static Round temporaryRound = new Round();

    public static Response response = new Response();
    // Создаем таблицу S0
    private static readonly byte[,] S0 = new byte[,]
    {
        {0x57, 0x49, 0xd1, 0xc6, 0x2f, 0x33, 0x74, 0xfb, 0x95, 0x6d, 0x82, 0xea, 0x0e, 0xb0, 0xa8, 0x1c},
        {0x28, 0xd0, 0x4b, 0x92, 0x5c, 0xee, 0x85, 0xb1, 0xc4, 0x0a, 0x76, 0x3d, 0x63, 0xf9, 0x17, 0xaf},
        {0xbf, 0xa1, 0x19, 0x65, 0xf7, 0x7a, 0x32, 0x20, 0x06, 0xce, 0xe4, 0x83, 0x9d, 0x5b, 0x4c, 0xd8},
        {0x42, 0x5d, 0x2e, 0xe8, 0xd4, 0x9b, 0x0f, 0x13, 0x3c, 0x89, 0x67, 0xc0, 0x71, 0xaa, 0xb6, 0xf5},
        {0xa4, 0xbe, 0xfd, 0x8c, 0x12, 0x00, 0x97, 0xda, 0x78, 0xe1, 0xcf, 0x6b, 0x39, 0x43, 0x55, 0x26},
        {0x30, 0x98, 0xcc, 0xdd, 0xeb, 0x54, 0xb3, 0x8f, 0x4e, 0x16, 0xfa, 0x22, 0xa5, 0x77, 0x09, 0x61},
        {0xd6, 0x2a, 0x53, 0x37, 0x45, 0xc1, 0x6c, 0xae, 0xef, 0x70, 0x08, 0x99, 0x8b, 0x1d, 0xf2, 0xb4},
        {0xe9, 0xc7, 0x9f, 0x4a, 0x31, 0x25, 0xfe, 0x7c, 0xd3, 0xa2, 0xbd, 0x56, 0x14, 0x88, 0x60, 0x0b},
        {0xcd, 0xe2, 0x34, 0x50, 0x9e, 0xdc, 0x11, 0x05, 0x2b, 0xb7, 0xa9, 0x48, 0xff, 0x66, 0x8a, 0x73},
        {0x03, 0x75, 0x86, 0xf1, 0x6a, 0xa7, 0x40, 0xc2, 0xb9, 0x2c, 0xdb, 0x1f, 0x58, 0x94, 0x3e, 0xed},
        {0xfc, 0x1b, 0xa0, 0x04, 0xb8, 0x8d, 0xe6, 0x59, 0x62, 0x93, 0x35, 0x7e, 0xca, 0x21, 0xdf, 0x47},
        {0x15, 0xf3, 0xba, 0x7f, 0xa6, 0x69, 0xc8, 0x4d, 0x87, 0x3b, 0x9c, 0x01, 0xe0, 0xde, 0x24, 0x52},
        {0x7b, 0x0c, 0x68, 0x1e, 0x80, 0xb2, 0x5a, 0xe7, 0xad, 0xd5, 0x23, 0xf4, 0x46, 0x3f, 0x91, 0xc9},
        {0x6e, 0x84, 0x72, 0xbb, 0x0d, 0x18, 0xd9, 0x96, 0xf0, 0x5f, 0x41, 0xac, 0x27, 0xc5, 0xe3, 0x3a},
        {0x81, 0x6f, 0x07, 0xa3, 0x79, 0xf6, 0x2d, 0x38, 0x1a, 0x44, 0x5e, 0xb5, 0xd2, 0xec, 0xcb, 0x90},
        {0x9a, 0x36, 0xe5, 0x29, 0xc3, 0x4f, 0xab, 0x64, 0x51, 0xf8, 0x10, 0xd7, 0xbc, 0x02, 0x7d, 0x8e}
    };

    private static readonly byte[,] S1 = new byte[,]
    {
        {0x6c, 0xda, 0xc3, 0xe9, 0x4e, 0x9d, 0x0a, 0x3d, 0xb8, 0x36, 0xb4, 0x38, 0x13, 0x34, 0x0c, 0xd9},
        {0xbf, 0x74, 0x94, 0x8f, 0xb7, 0x9c, 0xe5, 0xdc, 0x9e, 0x07, 0x49, 0x4f, 0x98, 0x2c, 0xb0, 0x93},
        {0x12, 0xeb, 0xcd, 0xb3, 0x92, 0xe7, 0x41, 0x60, 0xe3, 0x21, 0x27, 0x3b, 0xe6, 0x19, 0xd2, 0x0e},
        {0x91, 0x11, 0xc7, 0x3f, 0x2a, 0x8e, 0xa1, 0xbc, 0x2b, 0xc8, 0xc5, 0x0f, 0x5b, 0xf3, 0x87, 0x8b},
        {0xfb, 0xf5, 0xde, 0x20, 0xc6, 0xa7, 0x84, 0xce, 0xd8, 0x65, 0x51, 0xc9, 0xa4, 0xef, 0x43, 0x53},
        {0x25, 0x5d, 0x9b, 0x31, 0xe8, 0x3e, 0x0d, 0xd7, 0x80, 0xff, 0x69, 0x8a, 0xba, 0x0b, 0x73, 0x5c},
        {0x6e, 0x54, 0x15, 0x62, 0xf6, 0x35, 0x30, 0x52, 0xa3, 0x16, 0xd3, 0x28, 0x32, 0xfa, 0xaa, 0x5e},
        {0xcf, 0xea, 0xed, 0x78, 0x33, 0x58, 0x09, 0x7b, 0x63, 0xc0, 0xc1, 0x46, 0x1e, 0xdf, 0xa9, 0x99},
        {0x55, 0x04, 0xc4, 0x86, 0x39, 0x77, 0x82, 0xec, 0x40, 0x18, 0x90, 0x97, 0x59, 0xdd, 0x83, 0x1f},
        {0x9a, 0x37, 0x06, 0x24, 0x64, 0x7c, 0xa5, 0x56, 0x48, 0x08, 0x85, 0xd0, 0x61, 0x26, 0xca, 0x6f},
        {0x7e, 0x6a, 0xb6, 0x71, 0xa0, 0x70, 0x05, 0xd1, 0x45, 0x8c, 0x23, 0x1c, 0xf0, 0xee, 0x89, 0xad},
        {0x7a, 0x4b, 0xc2, 0x2f, 0xdb, 0x5a, 0x4d, 0x76, 0x67, 0x17, 0x2d, 0xf4, 0xcb, 0xb1, 0x4a, 0xa8},
        {0xb5, 0x22, 0x47, 0x3a, 0xd5, 0x10, 0x4c, 0x72, 0xcc, 0x00, 0xf9, 0xe0, 0xfd, 0xe2, 0xfe, 0xae},
        {0xf8, 0x5f, 0xab, 0xf1, 0x1b, 0x42, 0x81, 0xd6, 0xbe, 0x44, 0x29, 0xa6, 0x57, 0xb9, 0xaf, 0xf2},
        {0xd4, 0x75, 0x66, 0xbb, 0x68, 0x9f, 0x50, 0x02, 0x01, 0x3c, 0x7f, 0x8d, 0x1a, 0x88, 0xbd, 0xac},
        {0xf7, 0xe4, 0x79, 0x96, 0xa2, 0xfc, 0x6d, 0xb2, 0x6b, 0x03, 0xe1, 0x2e, 0x7d, 0x14, 0x95, 0x1d}
    };

    static uint[] CONs = new uint[60]
    {
        0xf56b7aeb, 0x994a8a42, 0x96a4bd75, 0xfa854521,
        0x735b768a, 0x1f7abac4, 0xd5bc3b45, 0xb99d5d62,
        0x52d73592, 0x3ef636e5, 0xc57a1ac9, 0xa95b9b72,
        0x5ab42554, 0x369555ed, 0x1553ba9a, 0x7972b2a2,
        0xe6b85d4d, 0x8a995951, 0x4b550696, 0x2774b4fc,
        0xc9bb034b, 0xa59a5a7e, 0x88cc81a5, 0xe4ed2d3f,
        0x7c6f68e2, 0x104e8ecb, 0xd2263471, 0xbe07c765,
        0x511a3208, 0x3d3bfbe6, 0x1084b134, 0x7ca565a7,
        0x304bf0aa, 0x5c6aaa87, 0xf4347855, 0x9815d543,
        0x4213141a, 0x2e32f2f5, 0xcd180a0d, 0xa139f97a,
        0x5e852d36, 0x32a464e9, 0xc353169b, 0xaf72b274,
        0x8db88b4d, 0xe199593a, 0x7ed56d96, 0x12f434c9,
        0xd37b36cb, 0xbf5a9a64, 0x85ac9b65, 0xe98d4d32,
        0x7adf6582, 0x16fe3ecd, 0xd17e32c1, 0xbd5f9f66,
        0x50b63150, 0x3c9757e7, 0x1052b098, 0x7c73b3a7
    };

    static byte[][] ConvertUintArrayToByteArrayArray()
    {
        byte[][] byteArrayArray = new byte[60][];

        for (int i = 0; i < byteArrayArray.Length; i++)
        {
            byteArrayArray[i] = BitConverter.GetBytes(CONs[i]);
            Array.Reverse(byteArrayArray[i]);
        }

        return byteArrayArray;
    }

    // Умножение в конечном поле GF(2^8)
    public static byte Mul2(byte x)
    {
        if ((x & 0x80) > 0)
        {
            x = (byte)(x ^ 0x0E);
        }
        byte res = (byte)((x << 1) | (x >> 7));
        return res;
    }

    public static byte Mul4(byte x) { return Mul2(Mul2(x)); }

    public static byte Mul6(byte x) { return (byte)(Mul2(x) ^ Mul4(x)); }

    public static byte Mul8(byte x) { return Mul2(Mul4(x)); }

    public static byte MulA(byte x) { return (byte)(Mul2(x) ^ Mul8(x)); }

    // Применение матриц распространения 
    public static byte[] ApplyDiffusionMatrix(byte[] f, int type)
    {
        byte[] result = new byte[f.Length];

        if (type == 0)
        {
            result[0] = (byte)(f[0] ^ Mul2(f[1]) ^ Mul4(f[2]) ^ Mul6(f[3]));
            result[1] = (byte)(Mul2(f[0]) ^ f[1] ^ Mul6(f[2]) ^ Mul4(f[3]));
            result[2] = (byte)(Mul4(f[0]) ^ Mul6(f[1]) ^ f[2] ^ Mul2(f[3]));
            result[3] = (byte)(Mul6(f[0]) ^ Mul4(f[1]) ^ Mul2(f[2]) ^ f[3]);
        }

        if (type == 1)
        {
            result[0] = (byte)(f[0] ^ Mul8(f[1]) ^ Mul2(f[2]) ^ MulA(f[3]));
            result[1] = (byte)(Mul8(f[0]) ^ f[1] ^ MulA(f[2]) ^ Mul2(f[3]));
            result[2] = (byte)(Mul2(f[0]) ^ MulA(f[1]) ^ f[2] ^ Mul8(f[3]));
            result[3] = (byte)(MulA(f[0]) ^ Mul2(f[1]) ^ Mul8(f[2]) ^ f[3]);
        }

        return result;
    }

    public static byte[] DoubleSwapFunction(byte[] L)
    {
        StringBuilder binaryStringBuilder = new StringBuilder();

        foreach (byte b in L)
        {
            string binaryString = Convert.ToString(b, 2).PadLeft(8, '0'); // Преобразование в двоичную строку и дополнение нулями слева
            binaryStringBuilder.Append(binaryString);
        }

        string binaryNumber = binaryStringBuilder.ToString();
        

        string firstSevenChars = binaryNumber.Substring(0, 7); // Вырезаем первые 7 символов
        string lastSevenChars = binaryNumber.Substring(binaryNumber.Length - 7); // Вырезаем последние 7 символов

        // удаляем первые 7 и последние 7 символов
        if (binaryNumber.Length >= 7 * 2)
        {
            binaryNumber = binaryNumber.Substring(7, binaryNumber.Length - 2 * 7);
        }

        string firstFiftySevenChars = binaryNumber.Substring(0, 57); // Вырезаем последние 57 символов

        string lastFiftySevenChars = binaryNumber.Substring(binaryNumber.Length - 57); // Вырезаем последние 57 символов
        
        // Составляем результат 
        string resultString = firstFiftySevenChars + lastSevenChars + firstSevenChars + lastFiftySevenChars;

        // Переводим бинарную строку в байты
        byte[] bytes = new byte[resultString.Length / 8];
        for (int i = 0; i < bytes.Length; i++)
        {
            string byteString = resultString.Substring(i * 8, 8);
            bytes[i] = Convert.ToByte(byteString, 2);
        }

        return bytes;
    }

    // Применение S-блоков
    public static byte[] ApplySBlocks(byte[] afterRoundKey, int initialState)
    {
        int sTable = initialState;

        List<byte> resultsList = new List<byte>();

        for (int i = 0; i < afterRoundKey.Length; i++)
        {
            byte result = FindIntersection(afterRoundKey[i], sTable);
            resultsList.Add(result);
            
            if (sTable == 0)
            {
                sTable++;
            } else
            {
                sTable--;
            }
            
        }

        byte[] resultsArray = resultsList.ToArray();
        return resultsArray;
    }

    public static byte[] XOR(byte[] plainBytes, byte[] roundKeyBytes)
    {
        if (plainBytes.Length != roundKeyBytes.Length)
        {
            throw new ArgumentException("Массивы должны иметь одинаковую длину.");
        }

        byte[] result = new byte[plainBytes.Length];
        for (int i = 0; i < plainBytes.Length; i++)
        {
            result[i] = (byte)(plainBytes[i] ^ roundKeyBytes[i]);
        }

        return result;
    }
    public static string ByteArrayToHexString(byte[][] byteArray)
    {
        StringBuilder hexString = new StringBuilder();
        foreach (byte[] array in byteArray)
        {
            hexString.Append(BitConverter.ToString(array).Replace("-", "").ToLower());
        }
        return hexString.ToString();
    }

    public static string StringToHex(string inputString)
    {
        var bytes = Encoding.UTF8.GetBytes(inputString);
        var hexString = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return hexString;
    }

    public static string HexStringToString(string hexString)
    {
        // Проверка на четное количество символов в строке
        if (hexString.Length % 2 != 0)
            throw new ArgumentException("Длина шестнадцатеричной строки должна быть четной");

        // Преобразование шестнадцатеричной строки в массив байтов
        byte[] bytes = new byte[hexString.Length / 2];
        for (int i = 0; i < hexString.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        // Преобразование массива байтов в строку с использованием кодировки UTF-8
        return Encoding.UTF8.GetString(bytes);
    }

    public static byte FindIntersection(byte ab, int sType)
    {
        byte a = (byte)((ab & 0xF0) >> 4); // Старший ниббл
        byte b = (byte)(ab & 0x0F);        // Младший ниббл
        // Преобразование символов в 4-битные значения
        byte result = 0;
        if (sType == 0)
        {
            result = S0[a, b]; // Поиск пересечения в таблице S0
        }

        if (sType == 1)
        {
            result = S1[a, b]; // Поиск пересечения в таблице S0
        }

        return result;
    }

    public static byte[] HexStringToByteArray(string hex)
    {
        int length = hex.Length;
        byte[] byteArray = new byte[length / 2];

        for (int i = 0; i < length; i += 2)
        {
            byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return byteArray;
    }

    private static byte[] GenerateRoundKey(byte[] currentL, int indexConstants, bool isOdd, byte[] currentK)
    {
        byte[] roundKey = new byte[16]; 
        byte[] constantsBytes = BitConverter.GetBytes(CONs[indexConstants]);

        Array.Reverse(constantsBytes);
        roundKey = XOR(currentL, constantsBytes);

        if (isOdd)
        {
            roundKey = XOR(roundKey, currentK);
        }

        return roundKey;
    }

    public static byte[][] GenerateRoundKeys(byte[] K)
    {
        byte[][] CONSForLGenerating = ConvertUintArrayToByteArrayArray();
        byte[][] key = new byte[4][];

        for (int i = 0; i < 4; i++)
        {
            key[i] = K.Skip(i * 4).Take(4).ToArray();
        }

        byte[][] L = GFN_(CONSForLGenerating, key, 12); // Генерация L
        int iterations = 35; // Количество итераций
        byte[][] roundKeys = new byte[iterations + 1][]; // Массив для хранения раундовых ключей в формате byte[]
        bool isOdd = false;
        byte[] LTemp = L[0].Concat(L[1]).Concat(L[2]).Concat(L[3]).ToArray();

        int indexConstants = 24;
        int counter = 0;

        // Проходим через все итерации
        for (int i = 0; i <= iterations; i++)
        {
            int startIndex = 4 * counter;

            // Выбираем текущую четверку байтов из L
            byte[] currentL = LTemp.Skip(startIndex).Take(4).ToArray();
            byte[] currentK = K.Skip(startIndex).Take(4).ToArray();

            // Генерация раундового ключа для текущей четверки байтов
            byte[] roundKey = GenerateRoundKey(currentL, indexConstants, isOdd, currentK);
            
            // Сохраняем раундовый ключ в массиве
            roundKeys[i] = roundKey;

            // Инвертируем значение isOdd после каждых 16 четверок
            if ((i + 1) % 4 == 0)
            {
                LTemp = DoubleSwapFunction(LTemp);
                counter = -1;
                isOdd = !isOdd;
            }

            indexConstants++;
            counter++;
        }

            return roundKeys;
    }

    // Ключевое забеливание
    public static byte[][] KeyWhitening(byte[] K, byte[] P, string type)
    {
        byte[][] result = new byte[4][];

        if (type == "initial")
        {
            result[0] = P.Take(4).ToArray();
            result[2] = P.Skip(8).Take(4).ToArray();
            result[1] = XOR(P.Skip(4).Take(4).ToArray(), K.Take(4).ToArray());
            result[3] = XOR(P.Skip(12).Take(4).ToArray(), K.Skip(4).Take(4).ToArray());
        }
        if (type == "final")
        {
            result[0] = P.Take(4).ToArray();
            result[1] = XOR(P.Skip(4).Take(4).ToArray(), K.Skip(8).Take(4).ToArray());
            result[2] = P.Skip(8).Take(4).ToArray();
            result[3] = XOR(P.Skip(12).Take(4).ToArray(), K.Skip(12).Take(4).ToArray());

        }
        return result;
    }
    public static byte[] F0(byte[] K, byte[] text, bool isForRes)
    {
        byte[] afterKeyRound = XOR(text, K);
        if (isForRes) temporaryRound.AfterKeyAdd += Regex.Replace(BitConverter.ToString(afterKeyRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");
        // Результат после применения S-блоков
        byte[] afterSRound = ApplySBlocks(afterKeyRound, 0);
        if (isForRes)  temporaryRound.AfterS += Regex.Replace(BitConverter.ToString(afterSRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");
        // Результат после применения матриц распространения
        byte[] afterMRound = ApplyDiffusionMatrix(afterSRound, 0);
        if (isForRes)  temporaryRound.AfterM += Regex.Replace(BitConverter.ToString(afterMRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");

        return afterMRound;
    }

    public static byte[] F1(byte[] K, byte[] text, bool isForRes)
    {
        byte[] afterKeyRound = XOR(text, K);
        if (isForRes)  temporaryRound.AfterKeyAdd += " " + Regex.Replace(BitConverter.ToString(afterKeyRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");
        // Результат после применения S-блоков
        byte[] afterSRound = ApplySBlocks(afterKeyRound, 1);
        if (isForRes)  temporaryRound.AfterS += " " + Regex.Replace(BitConverter.ToString(afterSRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");
        // Результат после применения матриц распространения
        byte[] afterMRound = ApplyDiffusionMatrix(afterSRound, 1);
        if (isForRes)  temporaryRound.AfterM += " " + Regex.Replace(BitConverter.ToString(afterMRound).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ");

        return afterMRound;
    }

    public static string BytesToString(byte[][] bytes)
    {
        string result = "";
        for (int i = 0; i < bytes.Length; i++)
        {
            result += BitConverter.ToString(bytes[i]).Replace("-", "").ToLower() + " ";
        }
        return result;
    }

    public static byte[][] GFN_(byte[][] roundKeys, byte[][] text, int rounds)
    {
        byte[][] input = text;

        int roundKeyCount = 0;
        for (int i = 0; i < rounds; i++)
        {
            bool isForRes = BytesToString(text) == response.AfterInitialWhitening;

            if (isForRes) temporaryRound.Input = BytesToString(input);
            if (isForRes) temporaryRound.RoundNumber = i + 1;
            // Переменная для хранения результата
            byte[][] result = new byte[4][];

            if (isForRes) temporaryRound.RoundKey = $"{BitConverter.ToString(roundKeys[roundKeyCount]).Replace("-", "").ToLower()} {BitConverter.ToString(roundKeys[roundKeyCount + 1]).Replace("-", "").ToLower()}";
            // Результат применения F-функций
            byte[] f0 = F0(roundKeys[roundKeyCount], input[0], isForRes);
            byte[] f1 = F1(roundKeys[roundKeyCount + 1], input[2], isForRes);

            if (isForRes)  response.Rounds.Add(temporaryRound);
            // Console.WriteLine(temporaryRound.AfterM);
            if (isForRes)  temporaryRound = new Round();

            if (i != rounds - 1)
            {
                result[0] = XOR(input[1], f0);
                result[1] = input[2];
                result[2] = XOR(input[3], f1);
                result[3] = input[0];
            }
            else
            {
                result[1] = XOR(input[1], f0);
                result[2] = input[2];
                result[3] = XOR(input[3], f1);
                result[0] = input[0];
            }

            input = result;
            roundKeyCount += 2;

            /* Console.WriteLine($"Round {i + 1}");
            for (int j = 0; j < result.Length; j++)
            {
                Console.Write(BitConverter.ToString(result[j]));
            }
            Console.WriteLine(); */
        }
        if (BytesToString(text) == response.AfterInitialWhitening)  response.Output = BytesToString(input);

        return input;
    }


    public static byte[][] CLEFIACipher(string K, string P, int rounds)
    {
        // Преобразовать строку keyBytes в массив байтов
        byte[] keyBytes = HexStringToByteArray(K);
        string[] keyDevided = Regex.Replace(BitConverter.ToString(keyBytes).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ").Split(" ");

        response.PlainText = Regex.Replace(P, ".{8}", "$0 "); ;
        response.InitialWhiteningKey = $"{keyDevided[0]} {keyDevided[1]}";
        response.FinalWhiteningKey = $"{keyDevided[2]} {keyDevided[3]}";

        // Преобразовать строку plainTextString в массив байтов
        byte[] plainTextBytes = HexStringToByteArray(P);
        // Ключевое забеливание 
        byte[][] plainTextAfterWhitening = KeyWhitening(keyBytes, plainTextBytes, "initial");

        response.AfterInitialWhitening = BytesToString(plainTextAfterWhitening);

        // Генерация ключей раундов
        byte[][] roundKeys = GenerateRoundKeys(keyBytes);

        byte[][] result = GFN_(roundKeys, plainTextAfterWhitening, rounds);
        // Ключевое забеливание
        byte[][] cipherText = KeyWhitening(keyBytes, result.SelectMany(bytes => bytes).ToArray(), "final");

        response.AfterFinalWhitening = BytesToString(cipherText);
        response.CipherText = BytesToString(cipherText);

        return cipherText;
    }

    public static Response ReturnEnctyptionResponse(bool isTextHex, bool isKeyHex, string key, string text)
    {
        response = new Response();

        string keyString = key;
        string textString = text;

        if (!isTextHex) textString = StringToHex(text);
        if (!isKeyHex) keyString = StringToHex(key);

        CLEFIACipher(keyString, textString, 18);
        return response;
    }

    public static byte[][] InverseCLEFIACipher(string K, string C, int rounds)
    {
        // Преобразовать строку keyBytes в массив байтов
        byte[] keyBytes = HexStringToByteArray(K);
        string[] keyDevided = Regex.Replace(BitConverter.ToString(keyBytes).Replace("-", "").ToLower(), ".{8}(?!$)", "$0 ").Split(" ");

        response.PlainText = Regex.Replace(C, ".{8}", "$0 "); ;
        response.InitialWhiteningKey = $"{keyDevided[0]} {keyDevided[1]}";
        response.FinalWhiteningKey = $"{keyDevided[2]} {keyDevided[3]}";
        // Преобразовать строку plainTextString в массив байтов
        byte[] plainTextBytes = HexStringToByteArray(C);
        // Ключевое забеливание 
        byte[][] plainTextAfterWhitening = KeyWhitening(keyBytes, plainTextBytes, "final");
        response.AfterInitialWhitening = BytesToString(plainTextAfterWhitening);

        // Генерация ключей раундов
        byte[][] roundKeys = GenerateRoundKeys(keyBytes);
        Array.Reverse(roundKeys);

        byte[][] result = InverseGFN_(roundKeys, plainTextAfterWhitening, rounds);

        // Ключевое забеливание
        byte[][] cipherText = KeyWhitening(keyBytes, result.SelectMany(bytes => bytes).ToArray(), "initial");

        response.AfterFinalWhitening = BytesToString(cipherText);
        response.CipherText = BytesToString(cipherText);

        return cipherText;
    }

    public static byte[][] InverseGFN_(byte[][] roundKeys, byte[][] text, int rounds)
    {
        byte[][] input = text;

        int roundKeyCount = 0;
        for (int i = 0; i < rounds; i++)
        {
            bool isForRes = BytesToString(text) == response.AfterInitialWhitening;
            if (isForRes) temporaryRound.Input = BytesToString(input);
            if (isForRes) temporaryRound.RoundNumber = i + 1;
            // Переменная для хранения результата
            byte[][] result = new byte[4][];
            if (isForRes) temporaryRound.RoundKey = $"{BitConverter.ToString(roundKeys[roundKeyCount]).Replace("-", "").ToLower()} {BitConverter.ToString(roundKeys[roundKeyCount + 1]).Replace("-", "").ToLower()}";


            // Результат применения F-функций
            byte[] f0 = F0(roundKeys[roundKeyCount + 1], input[0], isForRes);
            byte[] f1 = F1(roundKeys[roundKeyCount], input[2], isForRes);

            if (isForRes) response.Rounds.Add(temporaryRound);
            if (isForRes) temporaryRound = new Round();

            if (i != rounds - 1)
            {
                result[2] = XOR(input[1], f0);
                result[3] = input[2];
                result[0] = XOR(input[3], f1);
                result[1] = input[0];
            }
            else
            {
                result[1] = XOR(input[1], f0);
                result[2] = input[2];
                result[3] = XOR(input[3], f1);
                result[0] = input[0];
            }

            input = result;
            roundKeyCount += 2;
        }

        if (BytesToString(text) == response.AfterInitialWhitening) response.Output = BytesToString(input);

        return input;
    }

    public static Response ReturnDecryptedResult(bool isTextHex, bool isKeyHex, string key, string text)
    {
        response = new Response();

        string keyString = key;
        string textString = text;

        if (!isTextHex) textString = StringToHex(text);
        if (!isKeyHex) keyString = StringToHex(key);

        InverseCLEFIACipher(keyString, textString, 18);

        return response;
    }
}

