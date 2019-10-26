using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    //培训资格证明
  public  class TrainQualificationCertificate
    {
        public Guid ID { get; set; }
        public string Person { get; set; }
        public string CertificateNumber { get; set; }//证书编号
        public DateTime DateTime { get; set; }
        public string TrainingContent { get; set; }//培训内容(培训了什么，是否合格等)
    }
}
