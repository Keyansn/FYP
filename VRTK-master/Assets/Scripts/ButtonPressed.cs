namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class ButtonPressed : MonoBehaviour
    {
        int start=0;

        private VRTK_Button_UnityEvents buttonEvents;

        private void Start()
        {
            buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
            if (buttonEvents == null)
            {
                buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
            }
            buttonEvents.OnPushed.AddListener(handlePush);
        }

        private void handlePush(object sender, Control3DEventArgs e)
        {

            if (start > 0)
            {

                VRTK_Logger.Info("Pushed");
                InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
                Script.FreezeAll();
                print("pressed");
            }

            start = 1;
        }
    }
}