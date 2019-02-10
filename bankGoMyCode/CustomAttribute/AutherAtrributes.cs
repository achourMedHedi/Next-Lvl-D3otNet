using System;
using System.Collections.Generic;
using System.Text;

namespace bankGoMyCode.CustomAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    class AutherAttribute : Attribute
    {
        public string name { get; set; }
        public AutherAttribute(string n)
        {
            name = n;
        }
    }
}
