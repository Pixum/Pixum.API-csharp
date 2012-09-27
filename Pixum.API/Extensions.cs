using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pixum.API
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for RestRequest to allow calling AddParameter with an anonymous object.
        /// </summary>
        /// <example>
        /// var request = new RestRequest();
        /// request.AddParameter(new
        /// {
        ///     parameter1 = "Value 1",
        ///     parameter2 = "Value 2"
        /// });
        /// </example>
        /// <param name="request">RestRequest instance.</param>
        /// <param name="data">The object with properties.</param>
        /// <returns>IRestRequest</returns>
        public static IRestRequest AddParameter(this RestRequest request, object data)
        {
            var type = data.GetType();
            var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value = property.GetValue(data);

                if (value != null || (value is Nullable<int> && ((Nullable<int>)value).HasValue))
                {
                    request.AddParameter(property.Name, value.ToString());
                }
            }

            return request;
        }


        /// <summary>
        /// Genereate MD5 Hash for given input.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The generated MD5 Hash.</returns>
        public static string GetMd5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sBuilder.Append(hash[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
