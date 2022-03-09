namespace CarShowroom.Domain.Models.DTO
{
    public class CarWithUserDetails
    {
        public CarDto Car { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
