using LAB1.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1.Services
{
    public class HammingCodeService
    {
        private static int contorlBitsNum = 5;

        public string GenerateRandomError(string frame)
        {
            Random rnd = new Random();
            //if(rnd.Next(2) == 2)
            //{
                int index = rnd.Next(26);
                char insert = frame[index] == '1' ? '0' : '1';
                frame = frame.Remove(index, 1);
                frame = frame.Insert(index, insert.ToString());
                
            //}

            return frame;
        }

        private int calculateXorBit(String cadre)
        {
            StringBuilder result = new StringBuilder(cadre);
            int p = result[0] - 48;
            for (int i = 1; i < result.Length; i++)
            {
                p ^= result[i] - 48;
            }
            return p;
        }

        public string insertControlBits(string frame)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < contorlBitsNum; i++)
            {
                StringBuilder stringBuilder = new StringBuilder(frame);
                stringBuilder.Insert((int)Math.Pow(2, i) - 1, "0");
                frame = stringBuilder.ToString();
                
            }

            result.Append(frame).Append('0');

            return calculateControlBits(result.ToString());
        }

        private string calculateControlBits(string frame)
        {
            StringBuilder result = new StringBuilder(frame);
            for (int i = 0; i < contorlBitsNum; i++)
            {
                int start = (int)Math.Pow(2, i) - 1;
                int checkLength = (int)Math.Pow(2, i);
                int oneCount = 0;
                for (int j = start; j < result.Length - 1; j += 2 * checkLength)
                {
                    for (int length = 0; length < checkLength && j + length < result.Length - 1; length++)
                    {
                        if (start == j + length)
                        {
                            continue;
                        }
                        if (result[j+length] == '1')
                        {
                            oneCount++;
                        }
                    }
                }
                if (oneCount % 2 == 0)
                {
                    result[start] = '0';
                }
                else
                {
                    result[start] = '1';
                }
            }

            int p = calculateXorBit(result.ToString());
            result[result.Length - 1] = (char)(p + 48);
            return result.ToString();
        }

        public string deleteControlBits(string frame)
        {
            StringBuilder result = new StringBuilder(frame);
            for (int i = contorlBitsNum - 1; i >= 0; i--)
            {
                int index = (int)(Math.Pow(2, i) - 1);
                result.Remove(index, 1);
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public string CorrectMistakes(string frame)
        {
            char[] bits = frame.ToCharArray();
            int numParityBits = (int)Math.Ceiling(Math.Log(frame.Length + 1, 2));
            for (int i = 0; i < numParityBits; i++)
            {
                int parityBitPosition = (int)Math.Pow(2, i) - 1;
                int count = 0;

                for (int j = parityBitPosition; j < frame.Length; j += (parityBitPosition + 1) * 2)
                {
                    for (int k = j; k < j + parityBitPosition + 1 && k < frame.Length; k++)
                    {
                        if (bits[k] == '1')
                        {
                            count++;
                        }
                    }
                }

                if (count % 2 != 0)
                {
                    bits[parityBitPosition] = (bits[parityBitPosition] == '0') ? '1' : '0';
                }
            }

            return new string(bits);

        }

    }
}
