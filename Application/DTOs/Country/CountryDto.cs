namespace Fas7ny.Application.Dtos.CountryDtos
{
    public class CountryDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Currency { get; private set; }

        private CountryDto() { }

        public CountryDto(string name, string currency)
        {
            Id = Guid.NewGuid();
            Name = name;
            Currency = currency;
        }
    }

}
