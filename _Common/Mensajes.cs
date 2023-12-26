namespace Academia.SistemaGestionInventario.WApi._Common
{
    public class Mensajes
    {
        public const string FAIL_EMPTYORNULL = "El valor no puede estar vacío";
        public const string FAIL_ALREADY_EXIST = "El valor ya existe en la DB";
        public const string FAIL_NONVALUE_ZERO = "No se permiten valores menores a 1";
        public const string ERROR_AL_BUSCAR = "Error al encontrar el dato solicitado";
        public const string FAIL_DATA_NOT_EXIST = "No se encontro el registro solicitado";
        public const string FAIL_NOT_PERMISSIONS = "El empleado no cuenta con los permisos requeridos";
    }
}
