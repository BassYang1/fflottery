using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.Security
{
    public interface ICryptology
    {
        string Encrypt(string input);

        string Decrypt(string input);
    }
}
