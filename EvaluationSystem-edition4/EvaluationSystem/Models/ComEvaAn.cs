﻿namespace EvaluationSystem.Models
{
    public class ComEvaAn
    {
        //MaxScore代表F1分数中取值的上限
        //Aself代表自查分，Aaudit代表审核分，Astring代表审核意见
        public double MaxScore;
        private double Aself;
        private double Aaudit;
        public string ARemark;
        public double ASelf
        {
            get { return Aself; }
            set { if (value >= 0 && value <= MaxScore) { Aself = value; } }
        }
        public double AAudit
        {
            get { return Aaudit; }
            set { if (value >= 0 && value <= MaxScore) { Aaudit = value; } }
        }
    }
}
