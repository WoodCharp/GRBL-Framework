namespace GRBL.Wiki
{
    public static class WikiMessages
    {
        public static string GetMessageDescription(string rxData)
        {
            switch (rxData)
            {
                case "Reset to continue":
                    return "Reset is required before Grbl accepts any other commands.";
                case "'$H'|'$X' to unlock":
                    return "Unlock or do a homing.";
                case "Caution: Unlocked":
                    return "GRBL has been unlocked.";
                case "Enabled":
                    return "Enabled";
                case "Disabled":
                    return "Disabled";
                case "Check Door":
                    return "Safety door is open. Close the door.";
                case "Check Limits":
                    return "Limits are triggered instantly after power-up/reset. Check the limits.";
                case "Pgm End":
                    return "M2/30 program end message to denote g-code modes have been restored to defaults according to the M2/30 g-code description.";
                case "Restoring defaults":
                    return "Restoring defaults";
                case "Restoring spindle":
                    return "Appears when the spindle has been stopped during a feed hold via a spindle stop override command and when the cycle is resumed or the spindle stop is disabled.";
                case "Sleeping":
                    return "GRBL is in sleeping mode";
                default:
                    return string.Empty;
            }
        }
    }
}
