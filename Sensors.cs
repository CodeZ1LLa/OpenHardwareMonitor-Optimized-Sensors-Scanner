using OpenHardwareMonitor.Hardware;

namespace WindowsFormsApp3
{
    public class Sensors
    {

        public Sensors()
        {
            computer = new Computer()
            {
                CPUEnabled = true,
            };
            computer.Open();
        }

        public float CCX1Freq { get; private set; }
        public float CCX1Temp { get; private set; }
        public float CCX2Temp { get; private set; }
        public float CpuFrequency { get; private set; }
        public float CpuPPT { get; private set; }
        public float CpuTemp { get; private set; }
        public float CpuUsage { get; private set; }
        public float VCore { get; private set; }

        public void ReportSystemInfo()
        {
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                IHardware hardware = computer.Hardware[i];
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    // only fire the update when found
                    hardware.Update();

                    // loop through the data
                    for (int i1 = 0; i1 < hardware.Sensors.Length; i1++)
                    {
                        ISensor sensor = hardware.Sensors[i1];
                        switch (sensor.SensorType)
                        {
                            case SensorType.Temperature when sensor.Name.Contains("CPU Package"):
                                CpuTemp = sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Load when sensor.Name.Contains("CPU Total"):
                                CpuUsage = (int)sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Power when sensor.Name.Contains("CPU Package"):
                                CpuPPT = (int)sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Clock when sensor.Name.Contains("CPU Core #1"):
                                CpuFrequency = (int)sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Temperature when sensor.Name.Contains("CCD #1"):
                                CCX1Temp = (int)sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Temperature when sensor.Name.Contains("CCD #2"):
                                CCX2Temp = (int)sensor.Value.GetValueOrDefault();
                                break;

                            case SensorType.Voltage when sensor.Name.Contains("Voltage #1"):
                                VCore = sensor.Value.GetValueOrDefault();
                                break;
                        }
                    }
                }
            }
        }

        private Computer computer;
    }

}