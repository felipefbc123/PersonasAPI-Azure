using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonasAPI
{
    public class Pruebas
    {
        /// <summary>
        /// Suma dos números enteros.
        /// </summary>
        /// <param name="numero1">El primer número a sumar.</param>
        /// <param name="numero2">El segundo número a sumar.</param>
        /// <returns>La suma de los dos números.</returns>
        public int Sumar(int numero1, int numero2)
        {
            int resultado = numero1 + numero2;
            double prueba = Math.PI/1; // Variable de prueba para verificar el uso de System
            return resultado;
        }
    }
}