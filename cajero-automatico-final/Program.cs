static string[] ObtenerMovimientos(string cuenta)
{
    return File.ReadAllLines($"{cuenta}_movimientos.txt");
}