using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileManager : MonoBehaviour
{

    //Script to set Camara accordng to Phone

    public enum Device
    {
        SGS4,//1920x1080
        SGS5,//1920x1080
        SN8,//1280x800
        SGS3//1280x720
    }
    public Device _device;


    void Start()
    {

        //Camera.aspect = 16f/10f;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        switch (_device)
        {

            case (Device.SGS4):
                {
                    Screen.SetResolution(1920, 1080, true);
                    break;
                }
            case (Device.SGS5):
                {
                    Screen.SetResolution(1920, 1080, true);
                    break;
                }
            case (Device.SN8):
                {
                    Screen.SetResolution(1280, 800, true);
                    break;
                }
            case (Device.SGS3):
                {
                    Screen.SetResolution(1280, 720, true);
                    break;
                }

        }



    }

}
