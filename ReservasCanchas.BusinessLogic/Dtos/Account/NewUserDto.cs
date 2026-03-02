namespace ReservasCanchas.BusinessLogic.Dtos.Account
{
    //PASO 13 ESO DE TOKEN (paso 14 en account controller)
    public class NewUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
