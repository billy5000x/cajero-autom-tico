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