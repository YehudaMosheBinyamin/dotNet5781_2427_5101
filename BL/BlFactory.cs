using System;
using System.Collections.Generic;
using System.Text;
using BL;

namespace BlApi
{
    public static class BlFactory
    {
        public static IBL GetBl(string str)
        {
            switch (str)
            {
                case "1":
                    return new BlImplementation();
                default:
                    return new BlImplementation();
            }
        }

    }
}
