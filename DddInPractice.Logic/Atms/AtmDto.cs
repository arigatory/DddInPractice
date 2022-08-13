namespace DddInPractice.Logic.Atms
{
    public class AtmDto
    {
        public long Id { get; private set; }
        public int Cash { get; private set; }

        public AtmDto(long id, int cash)
        {
            Id = id;
            Cash = cash;
        }
    }
}
