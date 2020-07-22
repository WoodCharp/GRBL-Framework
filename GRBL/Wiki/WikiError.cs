namespace GRBL.Wiki
{
    public static class WikiError
    {
        public static string GetErrorDescription(int index)
        {
            switch (index)
            {
                case 1:
                    return "G-code words consist of a letter and a value. Letter was not found.";
                case 2:
                    return "Numeric value format is not valid or missing an expected value.";
                case 3:
                    return "Grbl '$' system command was not recognized or supported.";
                case 4:
                    return "Negative value received for an expected positive value.";
                case 5:
                    return "Homing cycle is not enabled via settings.";
                case 6:
                    return "Minimum step pulse time must be greater than 3usec.";
                case 7:
                    return "EEPROM read failed. Reset and restored to default values.";
                case 8:
                    return "Grbl '$' command cannot be used unless Grbl is IDLE. Ensures smooth operation during a job.";
                case 9:
                    return "G-code locked out during alarm or jog state.";
                case 10:
                    return "Soft limits cannot be enabled without homing also enabled.";
                case 11:
                    return "Max characters per line exceeded. Line was not processed and executed.";
                case 12:
                    return "(Compile Option) Grbl '$' setting value exceeds the maximum step rate supported.";
                case 13:
                    return "Safety door detected as opened and door state initiated.";
                case 14:
                    return "(Grbl-Mega Only) Build info or startup line exceeded EEPROM line length limit.";
                case 15:
                    return "Jog target exceeds machine travel. Command ignored.";
                case 16:
                    return "Jog command with no '=' or contains prohibited g-code.";
                case 17:
                    return "Laser mode requires PWM output.";
                case 20:
                    return "Unsupported or invalid g-code command found in block.";
                case 21:
                    return "More than one g-code command from same modal group found in block.";
                case 22:
                    return "Feed rate has not yet been set or is undefined.";
                case 23:
                    return "G-code command in block requires an integer value.";
                case 24:
                    return "Two G-code commands that both require the use of the XYZ axis words were detected in the block.";
                case 25:
                    return "A G-code word was repeated in the block.";
                case 26:
                    return "A G-code command implicitly or explicitly requires XYZ axis words in the block, but none were detected.";
                case 27:
                    return "N line number value is not within the valid range of 1 - 9,999,999.";
                case 28:
                    return "A G-code command was sent, but is missing some required P or L value words in the line.";
                case 29:
                    return "Grbl supports six work coordinate systems G54-G59. G59.1, G59.2, and G59.3 are not supported.";
                case 30:
                    return "The G53 G-code command requires either a G0 seek or G1 feed motion mode to be active. A different motion was active.";
                case 31:
                    return "There are unused axis words in the block and G80 motion mode cancel is active.";
                case 32:
                    return "A G2 or G3 arc was commanded but there are no XYZ axis words in the selected plane to trace the arc.";
                case 33:
                    return "The motion command has an invalid target. G2, G3, and G38.2 generates this error, if the arc is impossible to generate or if the probe target is the current position.";
                case 34:
                    return "A G2 or G3 arc, traced with the radius definition, had a mathematical error when computing the arc geometry. Try either breaking up the arc into semi-circles or quadrants, or redefine them with the arc offset definition.";
                case 35:
                    return "A G2 or G3 arc, traced with the offset definition, is missing the IJK offset word in the selected plane to trace the arc.";
                case 36:
                    return "There are unused, leftover G-code words that aren't used by any command in the block.";
                case 37:
                    return "The G43.1 dynamic tool length offset command cannot apply an offset to an axis other than its configured axis. The Grbl default axis is the Z-axis.";
                case 38:
                    return "Tool number greater than max supported value.";
                default:
                    return string.Empty;
            }
        }
    }
}
