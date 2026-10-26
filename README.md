# TaskN4
بصراحة يا هندسة ال ai ساعدنى بردو و رتبلى شوية معلومات بس الحمدلله فهمتها 
# Search task 
-1


        try
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine()!;

            CheckForVowels(input);

            Console.WriteLine("The string contains vowels.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void CheckForVowels(string input)
    {
        bool hasVowel = false;

        foreach (char ch in input)
        {
            char letter = char.ToLower(ch);

            if (letter == 'a' ||
                letter == 'e' ||
                letter == 'i' ||
                letter == 'o' ||
                letter == 'u')
            {
                hasVowel = true;
                break;
            }
        }

        if (!hasVowel)
        {
            throw new Exception("The string does not contain vowels.");
        }
------------------------------------------------------------------------------------------------
2-

        try
        {
            Console.WriteLine("Enter integers separated by spaces:");

            string input = Console.ReadLine()!;
            string[] numbers = input.Split(' ');

            HashSet<int> uniqueNumbers = new HashSet<int>();

            foreach (string number in numbers)
            {
                int value = Convert.ToInt32(number);

                if (!uniqueNumbers.Add(value))
                {
                    throw new Exception($"Duplicate number found: {value}");
                }
            }

            Console.WriteLine("All numbers are unique.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



