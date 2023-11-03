namespace WebJokosApp.Models
{
    public class Jokes
    {
        public int ID { get; set; }
        public string JokesPergunta { get; set;}
        public string JokesResposta { get; set;}

        public Jokes()
        {
        ID = 0;
        JokesPergunta = string.Empty;
        JokesResposta = string.Empty;
        }
    }
}
