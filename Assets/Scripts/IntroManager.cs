using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

namespace VRStandardAssets.Intro
{
    // The intro scene takes users through the basics
    // of interacting through VR in the other scenes.
    // This manager controls the steps of the intro
    // scene.
    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
        [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
        [SerializeField] private UIFader m_HowToUseFader;                   // This fader controls the UI showing how to use SelectionSliders.
        [SerializeField] private SelectionSlider m_HowToUseSlider;          // This is the slider that is used to demonstrate how to use them.
       	[SerializeField] private string m_MenuSceneName = "MainMenu";   // The name of the main menu scene.
		[SerializeField] private VRCameraFade m_VRCameraFade;           // Reference to the script that fades the scene to black.

        private IEnumerator Start ()
        {
            m_Reticle.Show ();
            
            m_Radial.Hide ();

            // In order, fade in the UI on how to use sliders, wait for the slider to be filled then fade out the UI.
            yield return StartCoroutine (m_HowToUseFader.InteruptAndFadeIn ());
            yield return StartCoroutine (m_HowToUseSlider.WaitForBarToFill ());
            yield return StartCoroutine (m_HowToUseFader.InteruptAndFadeOut ());

            // Fade in the final UI.
			yield return StartCoroutine (FadeToMenu ());
        }

		private IEnumerator FadeToMenu ()
		{
			// Wait for the screen to fade out.
			yield return StartCoroutine (m_VRCameraFade.BeginFadeOut (true));

			// Load the main menu by itself.
			SceneManager.LoadScene(m_MenuSceneName, LoadSceneMode.Single);
		}
    }
}