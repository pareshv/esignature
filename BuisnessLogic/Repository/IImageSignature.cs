using DataLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Repository
{
    public interface IImageSignature
    {
        public bool AddDigitalSignature(Pages PageObj,List<ImageSignParameters> ImgParamObj);
    }
}
