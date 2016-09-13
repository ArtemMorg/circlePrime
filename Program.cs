using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MIN_PRIME = 2;

            int maxNumber = 0;
            while(true)
            {
                try
                {
                    Console.Write("Enter maximum number: ");
                    maxNumber = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Wrong number! Try again");
                    continue;
                }
            }

            int p = MIN_PRIME;
            int power = p * p;

            List<Int32> prime = new List<Int32>();
            List<Int32> circlePrime = new List<Int32>();

            Console.WriteLine("Filling numbers to list...");
            for(int i=MIN_PRIME; i<= maxNumber; i++)
            {
                prime.Add(i);
                if((i % 10000) == 0)
                {
                    Console.WriteLine("{0}% proccessed with i={1}",(double)i/maxNumber*100,i);
                }
            }

            // Sieve of Eratosthenes algorithm
            Console.WriteLine("Looking for prime numbers...");
            while (power < maxNumber)
            {
                for(int i= power; i<= maxNumber; i+=p)
                {
                    prime.Remove(i);
                    if ((i % 10000) == 0)
                    {
                        Console.WriteLine("{0}% proccessed with p={1}", (double)i / maxNumber * 100, p);
                    }
                }
                p++;
                power = p * p;
                Console.WriteLine("proccessed with p={0}", p);
            }
            Console.WriteLine("All prime numbers recorded with count={0}.", prime.Count);
            Console.WriteLine("Looking for circle prime...");
            // Now we have all prime values. Collect only circle primes
            for(int i=0; i<prime.Count; i++)
            {
                if (isCirclePrime(prime[i], ref prime))
                {
                    circlePrime.Add(prime[i]);
                }
                if ((i % 100) == 0)
                {
                    Console.WriteLine("{0}% proccessed", Math.Round((double)i / prime.Count * 100,2));
                }
            }
            // Console.WriteLine(integers.Count);
            Console.WriteLine("Done. Circle primes are:");
            for (int i = 0; i < circlePrime.Count; i++)
            {
                Console.Write(circlePrime[i] + "  ");
            }
            Console.WriteLine("Total count = {0}", circlePrime.Count);
        }

        static bool isCirclePrime(int Value, ref List<Int32> Map)
        {
            // Logic is simple - convert int to string in order to simple replace number positions
            string value = Convert.ToString(Value);
            string currentValue = value;
            string checkValue = null;
            int indexPrime = -1;

            for(int i=0; i<value.Length; i++)
            {
                indexPrime = i;
                // Forming new number after interchanging numbers
                for (int j=0; j<value.Length; j++)
                {              
                    indexPrime++;
                    // If index is out of range - just null it
                    if (indexPrime < value.Length)
                    {
                       checkValue += value[indexPrime].ToString();
                    }
                    else
                    {
                        indexPrime = 0;
                        checkValue += value[indexPrime].ToString();
                    }               
                }
                // If new number is in prime base - continue changing. If not in base - return as false.
                if (Map.IndexOf(Convert.ToInt32(checkValue)) == -1)
                {
                    return false;
                }
                else
                {
                    checkValue = null;
                    continue;
                }
            }
            return true;
        }
    }
}
