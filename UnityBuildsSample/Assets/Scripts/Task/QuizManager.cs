using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public List<Quiz> quizzes;
    List<Quiz> quizzesMixed;
    int currentQuizIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitQuizes();
    }

    void InitQuizes()
    {
        quizzesMixed = new List<Quiz>();
        // Mix up the quizzes
        foreach (Quiz quiz in quizzes)
        {
            // Insert quiz in the random index
            int randomIndex = Random.Range(0, quizzesMixed.Count + 1);
            quizzesMixed.Insert(randomIndex, quiz);
        }
    }

    public Quiz GetCurrentQuiz()
    {
        if (IsQuizCompleted())
        {
            return null;
        }
        return quizzesMixed[currentQuizIndex];
    }

    public bool IsQuizCompleted()
    {
        return currentQuizIndex >= quizzesMixed.Count;
    }

    public bool IsQuizCorrect(int answerIndex)
    {
        if (IsQuizCompleted())
        {
            return false;
        }
        Quiz currentQuiz = quizzesMixed[currentQuizIndex];
        return currentQuiz.answerIndex == answerIndex;
    }

    public void ToNextQuestion()
    {
        if (IsQuizCompleted())
        {
            return;
        }
        currentQuizIndex++;
    }
}
