using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    public bool CurrentlySkipping {  get; private set; }
    public DialogueBox DialogueBox;

    private TMP_Text textBox;

    private int currentVisibleCharacterIndex;

    private Coroutine typewriterCoroutine;
    private Coroutine nextDialogueCoroutine;

    private WaitForSeconds simpleDelayTime;
    private WaitForSeconds interpunctuationDelayTime;
    private WaitForSeconds skipDelayTime;
    private WaitForSeconds nextDialogueDelayTime;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    [SerializeField] private float nextDialogueDelay = 5f;

    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();

        simpleDelayTime = new WaitForSeconds(1 / charactersPerSecond);
        interpunctuationDelayTime = new WaitForSeconds(interpunctuationDelay);
        skipDelayTime = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        nextDialogueDelayTime = new WaitForSeconds(nextDialogueDelay);
    }

    private void OnRightClick()
    {
        if(textBox.maxVisibleCharacters != textBox.textInfo.characterCount - 1)
        {
            Skip();
        }
    }

    public void SetText(string text)
    {
        if(typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private void Skip()
    {
        if (CurrentlySkipping)
        {
            return;
        }

        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(routine: SkipSpeedupReset());
            return;
        }

        StopCoroutine(typewriterCoroutine);
        TypingFinished();
    }

    private void TypingFinished()
    {
        if(currentVisibleCharacterIndex < textBox.textInfo.characterCount)
        {
            textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
        }

        if(CurrentlySkipping)
        {
            CurrentlySkipping = false;
        }

        nextDialogueCoroutine = StartCoroutine(routine: NextDialogueDelay());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo _textInfo = textBox.textInfo;

        while(currentVisibleCharacterIndex < _textInfo.characterCount + 1)
        {
            char character = _textInfo.characterInfo[currentVisibleCharacterIndex].character;

            textBox.maxVisibleCharacters++;

            if(!CurrentlySkipping && character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-')
            {
                yield return interpunctuationDelayTime;
            } else
            {
                yield return CurrentlySkipping ? skipDelayTime : simpleDelayTime;
            }

            currentVisibleCharacterIndex++;
            if(currentVisibleCharacterIndex == _textInfo.characterCount)
            {
                TypingFinished();
                break;
            }
        }
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }

    private IEnumerator NextDialogueDelay()
    {
        yield return nextDialogueDelayTime;
        DialogueBox.FinishDialogue();
        StopCoroutine(nextDialogueCoroutine);
    }
}
