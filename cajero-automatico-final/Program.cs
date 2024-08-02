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