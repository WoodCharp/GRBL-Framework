namespace GRBL.Wiki
{
    public static class WikiOPT
    {
        public static string GetOPTDescription(char letter)
        {
            switch (letter)
            {
                case 'V':
                    return "Variable spindle enabled";
                case 'N':
                    return "Line numbers enabled";
                case 'M':
                    return "Mist coolant enabled";
                case 'C':
                    return "CoreXY enabled";
                case 'P':
                    return "Parking motion enabled";
                case 'Z':
                    return "Homing force origin enabled";
                case 'H':
                    return "Homing single axis enabled";
                case 'T':
                    return "Two limit switches on axis enabled";
                case 'A':
                    return "Allow feed rate overrides in probe cycles";
                case '*':
                    return "Restore all EEPROM disabled";
                case '$':
                    return "Restore EEPROM $ settings disabled";
                case '#':
                    return "Restore EEPROM parameter data disabled";
                case 'I':
                    return "Build info write user string disabled";
                case 'E':
                    return "Force sync upon EEPROM write disabled";
                case 'W':
                    return "Force sync upon work coordinate offset change disabled";
                case 'L':
                    return "Homing init lock sets Grbl into an alarm state upon power up";
                default:
                    return string.Empty;
            }
        }
    }
}
