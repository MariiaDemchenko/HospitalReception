using Keras.Models;
using System;

namespace HospitalReception.MLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var model = Sequential.LoadModel("C://Users/mdemchenko/ML/Atherosclerosis/sens_06");
                //var model = Keras.Models.Model.LoadModel("C://Users/mdemchenko/ML/Atherosclerosis/sens_06");
                var entity = new double[] { 1, 2, 2, 2 };
                // var output = model.Predict(new Numpy.NDarray(entity));
            }
            catch (Exception ex)
            {
                var eee = ex;
            }
        }
    }
}
