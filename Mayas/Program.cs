using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayas
{
    class Program
    {
        /*
         * AUTOR: JUAN PABLO ISAZA MARÍN
         * FECHA: 17/09/2019
         * UNIVERSIDAD PONTIFICIA BOLIVARIANA
         * /


        /* declaración de variables globales */
        static Dictionary<string, int> h = crearTuplaH();
        static Dictionary<string, int> t = crearTuplaT();
        static StreamWriter file = new StreamWriter("salida.txt", true);

        public static void Main(string[] args)
        {
            /* aseguramos uso correcto con try-catch */
            try
            {
                /* obtenemos líneas del archivo */
                string[] lines = System.IO.File.ReadAllLines("test.txt");

                /* recorremos las líneas */
                foreach(string line in lines)
                {
                    /* aseguramos que la línea sea correcta */
                    if (entradaCorrecta(line))
                    {
                        /* hacemos split de la línea */
                        string[] s = line.Split(' ');

                        /* convertimos la fecha a número */
                        long fechaEnNumero = haabNum(s[0][0] - '0', s[1], s[2][0] - '0');

                        /* escribimos a un archivo la fecha en Tzolkin */
                        string resultado = imprimirFecha(fechaEnNumero);
                        file.WriteLine(resultado);
                    }
                }

                file.Close();
            }

            catch(Exception e)
            {
                /* mensaje y excepción */
                Console.WriteLine("Hubo un error en la ejecución del programa.");
                throw e;
            }
        }

        /*
         * Función que no recibe parámetros.
         * Retorna un diccionario con los meses del calendario HAAB.
         */
        static Dictionary<string, int> crearTuplaH()
        {
            Dictionary<string, int> h = new Dictionary<string, int>
            {
                { "pop", 0 },
                { "no", 20 },
                { "zip", 40 },
                {"zotz", 60},
                {"tzec", 80},
                {"zul", 100},
                {"yoxkin", 120},
                {"mol", 140},
                {"chen", 160},
                {"yax", 180},
                {"zac", 200},
                {"ceh", 220},
                {"mac", 240},
                {"kankin", 260},
                {"muan", 280},
                {"pax", 300},
                {"koyab", 320},
                {"cumhu", 340},
                {"uayet", 360}
            };

            return h;
        }

        /*
         * Función que no recibe parámetros.
         * Devuelve un diccionario con los meses del calendario Tzolkin.
         */

        static Dictionary<string, int> crearTuplaT()
        {
            Dictionary<string, int> t = new Dictionary<string, int>
            {
                {"imix", 1},
                {"ik", 2},
                {"akbal", 3},
                {"kan", 4},
                {"chichan", 5},
                {"cimi", 6},
                {"manik", 7},
                {"lamat", 8},
                {"muluk", 9},
                {"ok", 10},
                {"chuen", 11},
                {"eb", 12},
                {"ben", 13},
                {"ix", 14},
                {"mem", 15},
                {"cib", 16},
                {"caban", 17},
                {"eznab", 18},
                {"canac", 19},
                {"ahau", 20}
            };

            return t;
        }

        /*
         * Función que verifica las entradas del archivo de texto.
         * Recibe como parámetro una línea leída del archivo.
         * Devuelve verdadero en caso de que la línea sea una fecha en HAAB.
         * Devuelve falso en caso contrario.
         */ 

        static bool entradaCorrecta(string linea)
        {
            /* aseguramos funcionamiento correcto con try-catch */
            try
            {
                /* declaramos variables */
                string[] splitLinea = linea.Split(' ');

                /* las entradas del archivo son de tipo calendario HAAB, así:
                 * NúmeroDelDía. Mes Año.
                 * Al hacer el split, siempre debe dar un tamaño de 3,
                 * de lo contrario, no es entrada correcta
                 */
                if (splitLinea.Length != 3)
                    return false;

                else
                {
                    /* verificaciones de la entrada */
                    if (splitLinea[0].Length != 2)
                        return false;

                    if (splitLinea[0][1] != '.')
                        return false;

                    return true;
                }
            }
            
            catch(Exception e)
            {
                /* mensaje de error y excepción */
                Console.WriteLine("Error en las entradas.");
                throw e;
            }
        }

        /*
         * Función que devuelve la fecha HAAB en formato de número.
         * Recibe como parámetros el día, mes y año.
         * Devuelve un entero que es la fecha.
         */

        static long haabNum(int dia, string mes, int year)
        {
            int mesNum = h[mes]; // obtenemos el valor de la llave del diccionario h
            long l = 0;
            l += (dia + 1);
            l += mesNum;
            l += (year * 365);

            return l;
        }

        /*
         * Función que construye el string de la fecha en formato Tzolkin.
         * Recibe como parámetro la fecha HAAB convertida en número.
         */ 
        static string imprimirFecha(long fecha)
        {
            /* obtenemos la fecha */
            long year = (fecha / 260);
            long resta = fecha % 260;
            long valorMes = resta % 20;
            string myKey = t.FirstOrDefault(x => x.Value == valorMes).Key; // llave del mes por valor

            /* iteración para encontrar el día */
            long iterador = resta / 20;
            long dia = (valorMes % 13);
            for (int i = 0; i < iterador; i++)
            {
                if (dia + 7 == 13)
                {
                    dia = 13;
                }
                else
                {
                    dia = (dia + 7) % 13;
                }
            }

            /* construímos el string y lo retornamos */
            string resultado = dia + " " + myKey + " " + year;
            return resultado;
        }

    }
}
