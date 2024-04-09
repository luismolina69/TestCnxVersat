
using System;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using TestCnxVersat;

class Program
{
    
    static void Main()
    {
        Console.WriteLine("Conexión Bases Versat");
        Console.Write("Servidor: ");
        string servidor = Console.ReadLine();
        Console.Write("Base de Datos: ");
        string baseDatos = Console.ReadLine();
        Console.Write("Usuario: ");
        string usuario = Console.ReadLine();
        Console.Write("Contraseña: ");
        string contraseña = Console.ReadLine();

        //Convierte la contraseña en sha256 con separacion por espacios de subcadenas de longitud 8
        //Elemplo:
        //DC11EA98 A375F719 61998802 E8150C33 24F18B7C EB855B01 B6875273 58340259 
        string sha256Hash = UtilString.InsertnChar( HashPassword(contraseña).ToUpper(), string.Join("", Enumerable.Repeat(" ", 1)), 8);
        Console.WriteLine("SHA-256 hash del string: " + sha256Hash);


        //Crear la cadena de conexion
        StringBuilder builder = new StringBuilder();
        builder.Append(String.Format("Data Source={0};",servidor));
        builder.Append(String.Format("Initial Catalog={0};",baseDatos));
        builder.Append(String.Format("User ID={0};",usuario));
        builder.Append(String.Format("Password={0};", sha256Hash));

        string connectionString = builder.ToString();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Conexión exitosa");
            }
            else
            {
                Console.WriteLine("Error en la conexión");
            }
        }

    }

    /// <summary>
    ///  Optiene el SHA256 para el password de versat
    /// </summary>
    static string HashPassword(string psw) 
    {
        string cKey = "#BEL*2019-VS2.9.0.0#";
        return ObtenerSHA256(cKey.Substring(0, 4) + psw + cKey);
    }
    /// <summary>
    ///  Optiene el sha256 de un string dado
    /// </summary>
    static string ObtenerSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}



