using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTreeProject.GEDCOM.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Modified from http://stackoverflow.com/questions/1450774/splitting-a-string-into-chunks-of-a-certain-size
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(this string str, int chunkSize, string linePrefix = "")
        {
            int count = (int)Math.Ceiling((float)str.Length / chunkSize);

            List<string> splitStr = Enumerable.Range(0, count)
                .Select(i =>
                {
                    int startingPos = i * chunkSize;

                    // If we go over the length of the string, only get the last characters. 
                    // This prevents exceptions from strings that don't chunk evenly 
                    if (startingPos + chunkSize > str.Length)
                    {
                        return str.Substring(startingPos, str.Length - startingPos);
                    }

                    return str.Substring(startingPos, chunkSize);

                }).ToList();

            if (string.IsNullOrEmpty(linePrefix)) return splitStr;
            
            for (int i = 0; i < splitStr.Count(); i++)
            {
                splitStr[i] = linePrefix + splitStr;
            }
            

            return splitStr;
        }
    }
}
