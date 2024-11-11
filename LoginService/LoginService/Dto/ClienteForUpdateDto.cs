namespace LoginService.Dto
{
    public class ClienteForUpdateDto
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string direccion { get; set; }
        public string password { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public DateTime fechamodificacion { get; set; }
    }
}
