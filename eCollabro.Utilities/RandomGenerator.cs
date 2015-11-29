// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region references

using System;
using System.Text;

#endregion

namespace eCollabro.Utilities
{
    /// <summary>
    /// RandomGenerator
    /// </summary>
    public class RandomGenerator
    {
        /// <summary>
        /// RandomString
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
