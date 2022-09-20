using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSkinLib
{

    public enum CAMERA_CONTROL_TEXT_NAME
    {
        FILTER_TEXT,
        LEVEL_TEXT,
        GAIN_TEXT,
        SWIR_LEVEL_TEXT,
        SWIR_GAIN_TEXT,
        FLIR_LEVEL_TEXT,
        FLIR_GAIN_TEXT,

        LEVEL_TEXT_POPUP,
        GAIN_TEXT_POPUP,

        SWIR_LEVEL_TEXT_POPUP,
        SWIR_GAIN_TEXT_POPUP,
        FLIR_LEVEL_TEXT_POPUP,
        FLIR_GAIN_TEXT_POPUP,

        SEPERATOR_D,
        SEPERATOR_U,
        FREEZE_TEXT,
        SWIR_MODE_TEXT,
        SWIR_ENHACMENT_TEXT,
        FLIR_ENHACMENT_TEXT,
        DAY_ENHACMENT_TEXT,
        DAY_FILTER_TEXT,

        SWIR_FILTER_TEXT,
        FLIR_NUC_TABLE_TEXT,
        ON_OFF_TEXT,
        DATA_ON_OFF_TEXT,
        FILR_NUC_TABLE_TEXT,
        JOYSTICK_YAW,
        JOYSTICK_PITCH,
        JOYSTICK_YAW_REAL,
        JOYSTICK_PITCH_REAL
    }

    public enum JOYLIKE_BUTTON_NAME
    {
        JOYSTICK_HANDLER
    }

    public enum BUTTON_NAME
    {
        NONAME,
        SHOW_TARGETS,
        SHOW_QUERIES,
        SHOW_MISSIONS,
    }

    public class GSkinCommon
    {
        public static int UP = 1;
        public static int LEAVE = 0;
        public static int ENTER = 1;
        public static int DOWN = 2;
        public static int DISABLE = 3;

        public enum ACTIONS
        {
            UP = 1,
            LEAVE = 0,
            ENTER = 1,
            DOWN = 2,
            DISABLE = 3
        }
    }
    
    
}
