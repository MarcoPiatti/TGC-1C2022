using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP
{
    public class State
    {

    }
    /* 
     
    */
    public class Goma : State { 
    
    }
    /* 

    */
    public class Metal : State { }
    /* 
     Velocidad 90%
     Rebote = 0,7
     Salto = 0.8
     Peso = 2
     
    */
    public class Piedra : State { }
    /* 
     Velocidad 100%
     Rebote = 1 (normal)
     Salto = 1 
     Peso = 1
    */
    public class Plastico : State { }
    /* 
     Velocidad 200%
     Rebote = 1.5
     Salto = 1.5
     Peso =
     
    */



}
