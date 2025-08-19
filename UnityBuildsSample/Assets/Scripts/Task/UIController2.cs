using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController2 : MonoBehaviour
{
    // setup public variable of TextMeshProUGUI type field to display the question
    public TextMeshProUGUI questionText;
    public List<Button> buttons;
    public GameObject quizCompletePanel;
    public Button nextButton;
    public Button backToTitleButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InitFillQuiz());
        // Add event listens to each of the buttons (assuming they are answer buttons)
        for (int i = 0; i < buttons.Count; i++)
        {
            int answerIndex = i;
            Button button = buttons[i];
            button.onClick.AddListener(() =>
            {
                if (QuizManager.Instance.IsQuizCorrect(answerIndex))
                {
                    // Make the button color green in this case
                    button.GetComponent<Image>().color = Color.green;
                    // Make the next button interactable
                    nextButton.interactable = true;
                }
                else
                {
                    button.GetComponent<Image>().color = Color.red;
                }
            });
        }

        nextButton.onClick.AddListener(ToNextQuestion);

        // setup back to title button
        backToTitleButton.onClick.AddListener(() =>
        {
            // Load the title scene (assuming the title scene is named "Title")
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        // if completed quiz, press any key to go back to title scene
        if (QuizManager.Instance.IsQuizCompleted() && Input.anyKeyDown)
        {
            // Load the title scene (assuming the title scene is named "Title")
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }

    public void ToNextQuestion()
    {
        QuizManager quizManager = QuizManager.Instance;
        if (quizManager == null)
        {
            return;
        }
        quizManager.ToNextQuestion();
        if (quizManager.IsQuizCompleted())
        {
            // Show the quiz complete panel (set it active)
            quizCompletePanel.SetActive(true);
        } else
        {
            FillQuiz();
        }
    }

    IEnumerator InitFillQuiz()
    {
        yield return null;
        FillQuiz();
    }

    public void FillQuiz()
    {
        // Reset the button colors
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        // Disable the next button
        nextButton.interactable = false;
        Quiz currentQuiz = QuizManager.Instance?.GetCurrentQuiz();
        if (currentQuiz == null)
        {
            return;
        }
        // Set the question text
        questionText.text = currentQuiz.question;
        // Set the button texts
        for (int i = 0; i < buttons.Count; i++)
        {
            Button button = buttons[i];
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentQuiz.answers[i];
        }
    }
}
