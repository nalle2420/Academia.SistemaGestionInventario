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
        public const string LOGIN_FAIL = "Usuario o clave incorrecto";
        public const string NO_EXIST = "El dato que ingreso no existe en nuestro registro";
        public const string INVENTARIO_AGOTADO = "El producto seleccionado se encuentra completamente agotado";
        public const string PRODUCTO_INSUFICIENTE = "No existe producto suficiente para completar la salida";
        public const string FAIL_AGG = "No se pudo completar el ingreso";
        public const string NO_INGRESOS = "No se puede realizar el ingreso ya que la sucursal sobre pasa el limite de salidas";
        public const string NO_ESTADO = "No se puede realizar la solicitud pues los productos ya fueron recibidos";
        public const string REPORT_EMPTY = "La sucursal no tiene asignada salidas de producto en estas fechas";
        public const string PRODUCT_NOT_EXIST = "El producto seleccionado no existe";
        public const string USER_NOT_EXIST = "El usuario seleccionado no existe";
        public const string SUCURSAL_NOT_EXIST = "La sucursal seleccionada no existe";
        public const string LOTE_NOT_EXIST = "El lote seleccionado no existe";




    }
}
