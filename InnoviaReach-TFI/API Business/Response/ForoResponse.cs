﻿namespace API_Business.Response
{
    public class ForoResponse
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreVideoJuego { get; set; }
        public string NombreUsuarioCreador { get; set; }
        public DateTime FechaCreado { get; set; }
        public int Visitas { get; set; }
        public int Respuestas { get; set; }
        public bool Activo { get; set; }
    }
}
