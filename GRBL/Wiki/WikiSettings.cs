namespace GRBL.Wiki
{
    public static class WikiSettings
    {
        public static string GetIDDescription(int id)
        {
            switch(id)
            {
                case 0:
                    return "Step Pulse, microseconds";
                case 1:
                    return "Step idle delay, milliseconds";
                case 2:
                    return "Step port invert, mask";
                case 3:
                    return "Direction port invert, mask";
                case 4:
                    return "Step enable invert, boolean";
                case 5:
                    return "Limit pins invert, boolean";
                case 6:
                    return "Probe pin invert, boolean";
                case 10:
                    return "Status report, mask";
                case 11:
                    return "Junction deviation, mm";
                case 12:
                    return "Arc tolerance, mm";
                case 13:
                    return "Report inches, boolean";
                case 20:
                    return "Soft limits, boolean";
                case 21:
                    return "Hard limits, boolean";
                case 22:
                    return "Homing cycle, boolean";
                case 23:
                    return "Homing dir invert, mask";
                case 24:
                    return "Homing feed, mm/min";
                case 25:
                    return "Homing seek, mm/min";
                case 26:
                    return "Homing debounce, milliseconds";
                case 27:
                    return "Homing pull-off, mm";
                case 30:
                    return "Max spindle speed, RPM";
                case 31:
                    return "Min spindle speed, RPM";
                case 32:
                    return "Laser mode, boolean";
                case 100:
                    return "X steps/mm";
                case 101:
                    return "Y steps/mm";
                case 102:
                    return "Z steps/mm";
                case 110:
                    return "X Max rate, mm/min";
                case 111:
                    return "Y Max rate, mm/min";
                case 112:
                    return "Z Max rate, mm/min";
                case 120:
                    return "X Acceleration, mm/sec^2";
                case 121:
                    return "Y Acceleration, mm/sec^2";
                case 122:
                    return "Z Acceleration, mm/sec^2";
                case 130:
                    return "X Max travel, mm";
                case 131:
                    return "Y Max travel, mm";
                case 132:
                    return "Z Max travel, mm";
                default:
                    return string.Empty;
            }
        }
    }
}
