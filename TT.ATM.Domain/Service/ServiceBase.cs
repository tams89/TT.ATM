using TT.ATM.Domain.ObjectMapper;

namespace TT.ATM.Domain.Service
{
    public abstract class ServiceBase
    {
        public static readonly ATMContext Context;

        static ServiceBase()
        {
            Context = new ATMContext();
        }
    }
}