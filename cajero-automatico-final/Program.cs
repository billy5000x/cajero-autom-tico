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