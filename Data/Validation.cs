using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Data
{
    public class Validation
    {
        public class Pet
        {
            public const int MaxDescriptionLenght = 1000;
        }

        public class User
        {
            public const int MaxNameLenght = 20;
            public const int MaxBiographyLenght = 150;
        }
    }
}
