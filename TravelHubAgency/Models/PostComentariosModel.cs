namespace TravelHubAgency.Models
{
    public class PostComentariosModel
    {
        public Post Post { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<ReplayComentario> Replays { get; set; }
    }
}
