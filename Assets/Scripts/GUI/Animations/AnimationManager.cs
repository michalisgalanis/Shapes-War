using UnityEngine;

public class AnimationManager {

    public static GameObject ResetChildAnimations(GameObject parentUiElement, bool skipDelay) {
        if (parentUiElement == null) return null;
        ResetAnimation(parentUiElement, skipDelay);
        for (int i = 0; i < parentUiElement.transform.childCount; i++) {
            ResetChildAnimations(parentUiElement.transform.GetChild(i).gameObject, skipDelay);
        }
        return parentUiElement;
    }

    public static GameObject ResetAnimation(GameObject uiElement, bool skipDelay) {
        if (uiElement == null) return null;
        if (uiElement.GetComponent<EnlargeAnimation>() != null && uiElement.GetComponent<EnlargeAnimation>().enabled) {
            EnlargeAnimation animation = uiElement.GetComponent<EnlargeAnimation>();
            float delay = skipDelay ? 0f : animation.delay;
            bool isUI = animation.uiElement;
            Vector3 backupScale = animation.finalScale;
            Object.Destroy(animation);
            EnlargeAnimation newAnimation = uiElement.AddComponent<EnlargeAnimation>();
            newAnimation.delay = delay;
            newAnimation.uiElement = isUI;
            newAnimation.backupScale = backupScale;
        }
        if (uiElement.GetComponent <SliderAnimation>() != null && uiElement.GetComponent<SliderAnimation >().enabled) {
            SliderAnimation animation = uiElement.GetComponent<SliderAnimation>();
            float delay = skipDelay ? 0f : animation.delay;
            bool a = animation.animationFinished;
            float targetPercentage = animation.animationFinished ? -1f : animation.targetPercentage;
            Object.Destroy(animation);
            SliderAnimation newAnimation = uiElement.AddComponent<SliderAnimation>();
            newAnimation.delay = delay;
            newAnimation.targetPercentage = targetPercentage;
            //Debug.Log(newAnimation.name + "|| T.P: " + targetPercentage + ", Finished: " + a);
        }
        if (uiElement.GetComponent<TextAnimation>() != null && uiElement.GetComponent<TextAnimation>().enabled) {
            TextAnimation animation = uiElement.GetComponent<TextAnimation>();
            float delay = skipDelay ? 0f : animation.delay;
            bool a = animation.finished;
            //string backupStartString = animation.finalString;
            string startString = animation.finished ? animation.finalString : "";
            string finalString = animation.finished ? "" : animation.finalString;
            string backupStartString = startString.Equals("") ? animation.backupStartString : startString;
            string backupFinalString = animation.backupFinalString;
            //string backupFinalString = finalString.Equals("") ? animation.backupStartString : finalString;
            Object.Destroy(animation);
            TextAnimation newAnimation = uiElement.AddComponent<TextAnimation>();
            newAnimation.delay = delay;
            newAnimation.startString = startString;
            newAnimation.finalString = finalString;
            newAnimation.backupStartString = backupStartString;
            newAnimation.backupFinalString = backupFinalString;
            //Debug.Log(newAnimation.Equals(uiElement.GetComponent<UISmoothText>()));
            //Debug.Log(newAnimation.transform.parent.parent.parent.name + " || S: \"" + startString + "\"" + "|| F: \"" + finalString + "\"" + "|| BS: \"" + backupStartString + "\"|| BF: \"" + backupFinalString + "\"||" + (a ? " DONE" : ""));
        }
        return uiElement;
    }
}
