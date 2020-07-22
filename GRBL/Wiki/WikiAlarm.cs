namespace GRBL.Wiki
{
    public static class WikiAlarm
    {
        public static string GetAlarmDescription(int index)
        {
            switch (index)
            {
                case 1:
                    return "Hard limit triggered. Machine position is likely lost due to sudden and immediate halt. Re-homing is highly recommended.";
                case 2:
                    return "G-code motion target exceeds machine travel. Machine position safely retained. Alarm may be unlocked.";
                case 3:
                    return "Reset while in motion. Grbl cannot guarantee position. Lost steps are likely. Re-homing is highly recommended.";
                case 4:
                    return "Probe fail. The probe is not in the expected initial state before starting probe cycle, where G38.2 and G38.3 is not triggered and G38.4 and G38.5 is triggered.";
                case 5:
                    return "Probe fail. Probe did not contact the workpiece within the programmed travel for G38.2 and G38.4.";
                case 6:
                    return "Homing fail. Reset during active homing cycle.";
                case 7:
                    return "Homing fail. Safety door was opened during active homing cycle.";
                case 8:
                    return "Homing fail. Cycle failed to clear limit switch when pulling off. Try increasing pull-off setting or check wiring.";
                case 9:
                    return "Homing fail. Could not find limit switch within search distance. Defined as 1.5 * max_travel on search and 5 * pulloff on locate phases.";
                default:
                    return string.Empty;
            }
        }
    }
}
