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