namespace TT.ATM.Domain.Model
{
    public class Transaction : EntityBase
    {
        public int PinNumber { get; set; }
        public int Amount { get; set; }
    }
}