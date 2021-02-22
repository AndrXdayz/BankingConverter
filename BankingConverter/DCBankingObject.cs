using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingConverter
{
    public class DCBankingObject
    {
        public string m_PlainID { get; set; }
        public string m_Username { get; set; }
        public int m_OwnedCurrency { get; set; }

        public int m_MaxOwnedCurrencyBonus { get; set; }
    }
}
