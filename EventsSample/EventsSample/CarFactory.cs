using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsSample
{
    public delegate void CarInfo(string car);

    public class CarFactory
    {
        private CarInfo _carCreated;
        public event CarInfo CarCreated
        {
            add
            {
                _carCreated += value;
            }
            remove
            {
                _carCreated -= value;
            }
        }
        public void CreateACar(string car)
        {
            Console.WriteLine($"car {car} created");
            OnCarCreated(car);
        }

        private void OnCarCreated(string car)
        {
            // C# 5
            //CarInfo handler = _carCreated;
            //if (handler != null)
            //{
            //    handler(car);
            //}

            // C# 6
            //            _carCreated?.Invoke(car);

            CarInfo handler = _carCreated;
            if (handler != null)
            {
                var invocations = handler.GetInvocationList();
                foreach (CarInfo invocation in invocations)
                {
                    try
                    {

                        invocation?.Invoke(car);
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }
    }
}
