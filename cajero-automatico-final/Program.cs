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
