namespace TaskManagementService
{
    public class TestCodereviewSample
    {
        private void Test()
        {
            // Sample code to calculate the factorial of a number
            int number = 5;
            int factorial = CalculateFactorial(number);
            Console.WriteLine($"The factorial of {number} is: {factorial}");

            // Sample code to reverse a string
            string inputString = "hello";
            string reversedString = ReverseString(inputString);
            Console.WriteLine($"The reversed string of '{inputString}' is: '{reversedString}'");
        }
        public static int CalculateFactorial(int n)
        {
            if (n == 0)
                return 1;
            return n * CalculateFactorial(n - 1);
        }

        public static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
