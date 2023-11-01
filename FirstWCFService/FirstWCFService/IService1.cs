using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FirstWCFService
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "IService1"을 변경할 수 있습니다.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        double add(double num1,double num2);

        [OperationContract]
        double Sub(double num1, double num2);
        [OperationContract]
        double Mul(double num1, double num2);
        [OperationContract]
        double Div(double num1, double num2);

    }


}
