using System;
using System.IO;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

class CajeroAutomatico
{
    static string[,] cuentas = new string[3, 3];

    static void Main(string[] args)
    {
        // apertura de cuentas
        cuentas[0, 0] = "253090"; //Numero de cuenta
        cuentas[0, 1] = ("1346"); //clave
        cuentas[0, 2] = "1800000.00"; //saldo inicial 

        cuentas[1, 0] = "569543"; //Numero de cuentas 
        cuentas[1, 1] = ("1245"); //clave
        cuentas[1, 2] = "1250000.00"; //saldo

        cuentas[2, 0] = "794613"; //Numero de cuenta
        cuentas[2, 1] = ("8956"); //clave
        cuentas[2, 2] = "2000000.00"; //saldo

        string cuenta, contraseña;
        int cuentaRegistro;

        do
        {
            Console.WriteLine("Bienvenido al Cajero Automático");
            Console.WriteLine("Por favor ingrese su numero de cuenta");
            cuenta = Console.ReadLine();

            Console.WriteLine("Ingrese su Contraseña");
            contraseña = Console.ReadLine();

            cuentaRegistro = BuscarCuenta(cuenta, contraseña);

            if (cuentaRegistro != -1)
            {
                OperarCajero(cuentaRegistro);
            }
            else
            {
                Console.WriteLine("Numero de Cuenta o Contraseña incorrecta.  por favor, intentelo de nuevo.");
            }
        } while (true);
    }

    static void OperarCajero(int cuentaRegistro)
    {
        int opcion;

        do
        {
            System.Threading.Thread.Sleep(4000);
            Console.Clear();
            MostrarMenu();
            opcion = LeerOpcion();

            switch (opcion)
            {
                case 1:
                    RealizarRetiro(cuentaRegistro);
                    break;
                case 2:
                    MostrarMovimientos(cuentaRegistro);
                    break;
                case 3:
                    RealizarDeposito(cuentaRegistro);
                    break;
                case 4:
                    CambiarClave(cuentaRegistro);
                    break;
                case 5:
                    ConsultarSaldo(cuentaRegistro);
                    break;
                case 6:
                    RealizarTransferencia(cuentaRegistro);
                    break;
                case 7:
                    Console.WriteLine("Gracias por utlizar nuestros servios.");
                    return;
                default:
                    Console.WriteLine("Opción invalida por favor, seleccione una Opción valida.");
                    break;
            }
        } while (true);
    }

    static int BuscarCuenta(string cuenta, string contraseña)
    {
        for (int i = 0; i < cuentas.GetLength(0); i++)
        {
            if (cuentas[i, 0] == cuenta && VerificarContraseña(contraseña, cuentas[i, 1]))
            {
                return i;
            }
        }
        return -1;
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\nMenú de opciones:");
        Console.WriteLine("1. Retiro");
        Console.WriteLine("2. Movimientos");
        Console.WriteLine("3. Deposito");
        Console.WriteLine("4. Cambio de clave");
        Console.WriteLine("5. Consulta de saldo");
        Console.WriteLine("6. Transferencia");
        Console.WriteLine("7. salir ");
        Console.WriteLine("Seleccione una opción");
    }

    static int LeerOpcion()
    {
        int opcion;
        while (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Opción invalida. por favor, seleccione una opción valida.");
        }
        return opcion;
    }

    static void RealizarRetiro(int cuentaRegistro)
    {
        Console.WriteLine("Ingrese el monto a retirar.");
        double monto;
        while (!double.TryParse(Console.ReadLine(), out monto) || monto < 0)
        {
            Console.WriteLine("Monto invalido. Por favor, ingrese un monto valido.");
        }

        double saldo = double.Parse(cuentas[cuentaRegistro, 2]);

        if (monto > saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
            return;
        }

        saldo -= monto;
        cuentas[cuentaRegistro, 2] = saldo.ToString();
        Console.WriteLine($"Se a retirado ${monto}. Nuevo saldo: ${saldo}");
        string cuenta = cuentas[cuentaRegistro, 0];
        string[] reti = { $"Se ha retirado ${monto}. Nuevo saldo: ${saldo}" };
        File.AppendAllLines($"{cuenta}_Factura-electronica.txt", reti);
    }

    static void MostrarMovimientos(int cuentaRegistro)
    {
        string cuenta = cuentas[cuentaRegistro, 0];
        string[] movimientos = ObtenerMovimientos(cuenta);

        Console.WriteLine("Movimientos recientes:");

        foreach (string movimiento in movimientos)
        {
            Console.WriteLine(movimiento);
        }
    }

    static string[] ObtenerMovimientos(string cuenta)
    {
        return File.ReadAllLines($"{cuenta}_movimientos.txt");
    }

    static void RealizarDeposito(int cuentaRegistro)
    {
        Console.WriteLine("ingrese el monto a deposotar");
        double monto;
        while (!double.TryParse(Console.ReadLine(), out monto) || monto <= 0)
        {
            Console.WriteLine("monto invalido por favor ingrese un monto valido");
        }

        double saldo = double.Parse(cuentas[cuentaRegistro, 2]);

        saldo += monto;
        cuentas[cuentaRegistro, 2] = saldo.ToString();
        Console.WriteLine($"Se ha depositado ${monto}. Nuevo saldo: ${saldo}");

        string cuenta = cuentas[cuentaRegistro, 0];
        string[] movimiento = { $"Deposito: ${monto}. Nuevo saldo: ${saldo}" };
        File.AppendAllLines($"{cuenta}_movimientos.txt", movimiento);
    }

    static void CambiarClave(int cuentaRegistro)
    {
        Console.WriteLine("Ingrese su nueva clave:");
        string nuevaClave = Console.ReadLine();
        cuentas[cuentaRegistro, 1] = (nuevaClave); // actualizar la contraseña 
        Console.WriteLine("Clave cambiada correctamente.");
    }

    static void ConsultarSaldo(int cuentaRegistro)
    {
        double saldo = double.Parse(cuentas[cuentaRegistro, 2]);
        Console.WriteLine($"Su saldo actual es: ${saldo}");
    }

    static void RealizarTransferencia(int cuentaRegistro)
    {
        Console.WriteLine("Ingrese el numero de cuenta al que desea hacer la transferencia:");
        string CuentaDestino = Console.ReadLine();

        int CuentaDestinoRegistro = BuscarCuentaDestino(CuentaDestino);

        if (cuentaRegistro == -1)
        {
            Console.WriteLine("La cuenta destino no existe");
            return;
        }

        Console.WriteLine("ingrese el monto a transferir:");
        double monto;
        while (!double.TryParse(Console.ReadLine(), out monto) || monto <= 0)
        {
            Console.WriteLine("monto invalido. por favor, ingrese un monto valido.");
        }

        double saldoOrigen = double.Parse(cuentas[cuentaRegistro, 2]);
        double saldoDestino = double.Parse(cuentas[CuentaDestinoRegistro, 2]);

        if (monto > saldoOrigen)
        {
            Console.WriteLine("saldo insuficiente para hacer la transferencia.");
            return;
        }

        saldoOrigen -= monto;
        saldoDestino += monto;

        cuentas[cuentaRegistro, 2] = saldoOrigen.ToString();
        cuentas[CuentaDestinoRegistro, 2] = saldoDestino.ToString();

        Console.WriteLine($"Se ha transferido ${monto} a la cuenta {cuentas[CuentaDestinoRegistro, 0]}. Nuevo saldo: ${saldoOrigen}");

        using (StreamWriter fac = new StreamWriter("C:\\Users\\User\\OneDrive - Politécnico Grancolombiano\\Escritorio\\Factura-electronica.txt", true))
        {
            fac.WriteLine($"Fecha: {DateTime.Now}");
            fac.WriteLine("----------------------------------------------");
            fac.WriteLine("|Factura - CUENTA: {0}", cuentas[cuentaRegistro, 0]);
            fac.WriteLine("----------------------------------------------");
            fac.WriteLine("|Destino de la transferencia : {0}", cuentas[CuentaDestinoRegistro, 0]);
            fac.WriteLine("COMPROBANTE DE TRANFERENCIA");
            fac.WriteLine("----------------------------------------------");
            fac.WriteLine("Saldo a transferir: ${0}", monto);
            fac.WriteLine("---------------------------------------");
            fac.WriteLine("Saldo actual: ${0}", saldoOrigen);
            fac.WriteLine("---------------------------------------");
        }
    }


    static int BuscarCuentaDestino(string cuentaDestino)     
    {
        for (int i = 0; i < cuentas.GetLength(0); i++)
        {
            if (cuentas[i, 0] == cuentaDestino)
            {
                return i;
            }
        }
        return -1;
    }


    // Función para verificar la contraseña

    static bool VerificarContraseña(string ContraseñaIngresada, string ContraseñaAlmacenada)
    {
        return ContraseñaIngresada == ContraseñaAlmacenada;
    }
}
