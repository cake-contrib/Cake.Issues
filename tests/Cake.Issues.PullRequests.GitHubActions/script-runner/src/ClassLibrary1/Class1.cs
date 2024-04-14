using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        public void Foo()
        {
            var foo = "foo";
            var bar = "bar";
            if (!string.IsNullOrEmpty(foo) && !string.IsNullOrEmpty(bar))
            {
                var foobar = foo + bar;
            }
        }

        public void Bar()
        {
            var foo = "foo";
            var bar = "bar";
            if (!string.IsNullOrEmpty(foo) && !string.IsNullOrEmpty(bar))
            {
                var foobar = foo + bar;
            }
        }
    }
}
