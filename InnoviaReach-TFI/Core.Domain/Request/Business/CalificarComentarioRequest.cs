namespace API_Business.Request
{
    public class CalificarComentarioRequest
    {
        public int Comentario_ID { get; set; }
        public string User_ID { get; set; }
        public int Puntuacion { get; set; }
    }
}
