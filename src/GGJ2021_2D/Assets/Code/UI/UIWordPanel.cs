using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWordPanel : MonoBehaviour
{
    [System.Serializable]
    public class LetterAnimator
    {
        public Image m_image;
        public float m_elapsedTime;
        public float m_delay;
        public bool m_done;

        public void Reset()
        {
            m_elapsedTime = 0;
            m_done = false;
        }
    }

    [Header("English")]
    public Text m_englishText;

    [SerializeField]
    private List<LetterAnimator> m_sentence = new List<LetterAnimator>();

    public void Setup(string word)  
    {
        // Set english text
        m_englishText.text = word;

        // Setup alien characters
        for (int i = 0; i < m_sentence.Count; i++)
        {
            // Get char
            char this_char = (i < word.Length ) ? word[i] : word[word.Length-1];

            // Setup
            m_sentence[i].Reset();
            Utils.SetImageAlpha(m_sentence[i].m_image, 1);
            m_sentence[i].m_image.sprite = UIController.Instance.GetSpriteForLetter(this_char);

            // Calc target time for transition end
            float targetTime = (i + 1) * UIConversationController.LETTER_TRANSITION_TIME;
            m_sentence[i].m_delay = targetTime - UIConversationController.LETTER_TRANSITION_TIME;
            
        }
    }
}