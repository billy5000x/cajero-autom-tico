static void ConsultarSaldo(int cuentaRegistro)
{
    double saldo = double.Parse(cuentas[cuentaRegistro, 2]);
    Console.WriteLine($"Su saldo actual es: ${saldo}");
}