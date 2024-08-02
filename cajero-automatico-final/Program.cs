static int LeerOpcion()
{
    int opcion;
    while (!int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.WriteLine("opción invalida. por favor, seleccione una opción valida.");
    }
    return opcion;
}