namespace RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos
{
    public class ActivationCodeListDto
    {
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public string ActivatedDate { get; set; }
        public string ActivetedUserName { get; set; }
        public string ActivetedUserLastName { get; set; }
        public string CreatedDate { get; set; }
    }
}
