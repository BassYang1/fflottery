using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottery.Entity
{
    [Serializable]
    public class LotteryDataModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string NumberAll { get; set; }
        public int Total { get; set; }
        public int Dx { get; set; }
        public int Ds { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime STime { get; set; }
        public int Flag { get; set; }
        public int Flag2 { get; set; }
        public bool IsFill { get; set; }
    }
}
