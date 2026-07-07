using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Core
{
    public enum Flag
    {
        Too_Many_Reviews, 
        Movie_Not_Found,
        Actor_Not_Found,
        MovieActor_Exists,
        OK
    }
}
