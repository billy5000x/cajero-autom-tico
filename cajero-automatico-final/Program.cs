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
